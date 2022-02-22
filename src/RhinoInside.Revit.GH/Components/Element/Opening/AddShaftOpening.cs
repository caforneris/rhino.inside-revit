using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using ARDB = Autodesk.Revit.DB;
using RIR = RhinoInside.Revit;
using RhinoInside.Revit.External.DB.Extensions;

namespace RhinoInside.Revit.GH.Components.Openings
{
  public class AddShaftOpening : ElementTrackerComponent
  {
    public override Guid ComponentGuid => new Guid("657811B7-6662-4FCF-A67A-A65C34FA0651");
    public override GH_Exposure Exposure => GH_Exposure.primary | GH_Exposure.obscure;
    protected override string IconTag => string.Empty;

    public AddShaftOpening() : base
    (
      name: "Add Shaft Opening",
      nickname: "Shaft",
      description: "Given its outline curve, it adds a Shaft opening to the active Revit document",
      category: "Revit",
      subCategory: "Host"
    )
    { }


    protected override ParamDefinition[] Inputs => inputs;
    static readonly ParamDefinition[] inputs =
    {
      new ParamDefinition
      (
        new Param_Curve
        {
          Name = "Boundary",
          NickName = "B",
          Description = $"Boundary to create the shaft opening"
        }
      ),
      new ParamDefinition
      (
        new Parameters.Level
        {
          Name = "Base Constraint",
          NickName = "BC",
          Description = $"Level to constraint the base of the opening",
        }
      ),
      new ParamDefinition
      (
        new Param_Number
        {
          Name = "Base Constraint Offset",
          NickName = "BCO",
          Description = $"Offset to the level of the base of the opening",
          Optional = true
        }, ParamRelevance.Occasional
      ),
      new ParamDefinition
      (
        new Parameters.Level
        {
          Name = "Top Constraint",
          NickName = "TC",
          Description = $"Level to constraint the top of the opening",
        }
      ),
      new ParamDefinition
      (
        new Param_Number
        {
          Name = "Top Constraint Offset",
          NickName = "TCO",
          Description = $"Offset to the level of the top of the opening",
          Optional = true
        }, ParamRelevance.Occasional
      ),
    };

    protected override ParamDefinition[] Outputs => outputs;
    static readonly ParamDefinition[] outputs =
    {
      new ParamDefinition
      (
        new Parameters.GraphicalElement()
        {
          Name = _ShaftOpening_,
          NickName = _ShaftOpening_.Substring(0, 1),
          Description = $"Output {_ShaftOpening_}",
        }
      )
    };

    const string _ShaftOpening_ = "Shaft Opening";

    protected override void TrySolveInstance(IGH_DataAccess DA)
    {
      if (!Parameters.Document.TryGetDocumentOrCurrent(this, DA, "Document", out var doc) || !doc.IsValid) return;

      ReconstructElement<ARDB.Opening>
      (
        doc.Value, _ShaftOpening_, (opening) =>
        {
          // Input
          if (!Params.GetData(DA, "Boundary", out Curve boundary)) return null;
          if (!Params.GetData(DA, "Base Constraint", out ARDB.Level baseLevel)) return null;
          if (!Params.TryGetData(DA, "Base Constraint Offset", out double? baseOffset)) return null;
          if (!Params.GetData(DA, "Top Constraint", out ARDB.Level topLevel)) return null;
          if (!Params.TryGetData(DA, "Top Constraint Offset", out double? topOffset)) return null;

          // Compute
          StartTransaction(doc.Value);
          {
            opening = Reconstruct(opening, doc.Value, boundary, baseLevel, baseOffset.HasValue ? baseOffset.Value : 0.0 , topLevel, topOffset.HasValue ? topOffset.Value : 0.0);
          }

          DA.SetData(_ShaftOpening_, opening);
          return opening;
        }
      );
    }

    bool Reuse(ARDB.Opening opening, ARDB.Document doc, Curve boundary, ARDB.Level baseLevel, double baseOffset, ARDB.Level topLevel, double topOffset)
    {
      if (opening is null) return false;

      if (opening.BoundaryCurves is ARDB.CurveArray oldBoundary)
      {
        var level = opening.Document.GetElement(opening.LevelId) as ARDB.Level;
        var levelPlane = new Plane(new Point3d(0.0, 0.0, level.GetHeight() * Revit.ModelUnits), Vector3d.ZAxis);
        boundary = Curve.ProjectToPlane(boundary, levelPlane);

        var newBoundary = Convert.Geometry.GeometryEncoder.ToCurveArray(boundary);

        if (newBoundary.Size != oldBoundary.Size) return false;

        for (int c = 0; c < newBoundary.Size; ++c)
        {
          if (!newBoundary.get_Item(c).IsAlmostEqualTo(oldBoundary.get_Item(c)))
            return false;
        }
      }

      return true;
    }

    ARDB.Opening Reconstruct(ARDB.Opening opening, ARDB.Document doc, Curve boundary, ARDB.Level baseLevel, double baseOffset, ARDB.Level topLevel, double topOffset)
    {
      if (!Reuse(opening, doc, boundary, baseLevel, baseOffset, topLevel, topOffset))
      {
        opening = Create(doc, baseLevel, topLevel, boundary);
      }

      opening.get_Parameter(ARDB.BuiltInParameter.WALL_BASE_OFFSET).Update(baseOffset);
      opening.get_Parameter(ARDB.BuiltInParameter.WALL_TOP_OFFSET).Update(topOffset);

      return opening;
    }

    ARDB.Opening Create(ARDB.Document doc, ARDB.Level baseLevel, ARDB.Level topLevel, Curve b)
    {
      var opening = default(ARDB.Opening);

      if (opening is null)
      {
        ARDB.CurveArray boundary = RIR.Convert.Geometry.GeometryEncoder.ToCurveArray(b);
        opening = doc.Create.NewOpening(baseLevel, topLevel, boundary);

      }

      return opening;
    }

  }
}
