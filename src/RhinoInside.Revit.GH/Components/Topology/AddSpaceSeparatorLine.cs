using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using RhinoInside.Revit.Convert.Geometry;
using RhinoInside.Revit.External.DB.Extensions;
using ARDB = Autodesk.Revit.DB;

namespace RhinoInside.Revit.GH.Components.SpatialElements
{
  [ComponentVersion(introduced: "1.7")]
  public class AddSpaceSeparatorLine : ElementTrackerComponent
  {
    public override Guid ComponentGuid => new Guid("DEA31165-A184-466F-9119-D726472B226E");
    public override GH_Exposure Exposure => GH_Exposure.quarternary;
    protected override string IconTag => string.Empty;

    public AddSpaceSeparatorLine() : base
    (
      name: "Add Space Separation",
      nickname: "SpaceSeparation",
      description: "Given the curve, it adds a Space separatoion line to the given Revit view",
      category: "Revit",
      subCategory: "Topology"
    )
    { }

    protected override ParamDefinition[] Inputs => inputs;
    static readonly ParamDefinition[] inputs =
    {
      new ParamDefinition
      (
        new Parameters.View()
        {
          Name = "View",
          NickName = "V",
          Description = "View to add a specific a room separator line",
          Access = GH_ParamAccess.item
        }
      ),
      new ParamDefinition
      (
        new Param_Curve
        {
          Name = "Curve",
          NickName = "C",
          Description = "Curves to create a specific space separation line",
          Access = GH_ParamAccess.item
        }
      )
    };

    protected override ParamDefinition[] Outputs => outputs;
    static readonly ParamDefinition[] outputs =
    {
      new ParamDefinition
      (
        new Parameters.CurveElement()
        {
          Name = _SpaceSeparation_,
          NickName = _SpaceSeparation_.Substring(0, 1),
          Description = $"Output {_SpaceSeparation_}",
          Access = GH_ParamAccess.item
        }
      )
    };

    const string _SpaceSeparation_ = "Space Separation";

    protected override void TrySolveInstance(IGH_DataAccess DA)
    {
      if (!Params.GetData(DA, "View", out ARDB.View view)) return;
      if (!(view is ARDB.ViewPlan))
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "View should be a plan view");
        return;
      }

      ReconstructElement<ARDB.ModelCurve>
      (
        view.Document, _SpaceSeparation_, (spaceSeparatorLine) =>
        {
          // Input
          if (!Params.GetData(DA, "Curve", out Curve curve)) return null;

          var tol = GeometryObjectTolerance.Model;
          if
          (
            curve.IsShort(tol.ShortCurveTolerance) ||
            curve.IsClosed ||
            !curve.TryGetPlane(out var plane, tol.VertexTolerance) ||
            plane.ZAxis.IsParallelTo(Vector3d.ZAxis, tol.AngleTolerance) == 0
          )
            throw new Exceptions.RuntimeArgumentException("Curve", "Curve should be a valid horizontal, planar and open curve.", curve);

          // Compute
          spaceSeparatorLine = Reconstruct(spaceSeparatorLine, view as ARDB.ViewPlan, curve);

          DA.SetData(_SpaceSeparation_, spaceSeparatorLine);
          return spaceSeparatorLine;
        }
      );
    }

    bool Reuse(ARDB.ModelCurve spaceSeparator, ARDB.ViewPlan view, Curve curve)
    {
      if (spaceSeparator is null) return false;

      var genLevel = view.GenLevel;
      if (spaceSeparator.LevelId != genLevel?.Id) return false;

      var levelPlane = Plane.WorldXY;
      levelPlane.Translate(Vector3d.ZAxis * genLevel.GetElevation() * Revit.ModelUnits);

      using (var projectedCurve = Curve.ProjectToPlane(curve, levelPlane).ToCurve())
      {
        if (!projectedCurve.IsAlmostEqualTo(spaceSeparator.GeometryCurve))
          spaceSeparator.SetGeometryCurve(projectedCurve, overrideJoins: true);
      }

      return true;
    }

    ARDB.ModelCurve Create(ARDB.ViewPlan view, Curve curve)
    {
      if (view.GenLevel is ARDB.Level level)
      {
        var sketchPlane = level.GetSketchPlane(ensureSketchPlane: true);
        using (var projectedCurve = Curve.ProjectToPlane(curve, sketchPlane.GetPlane().ToPlane()))
        using (var curveArray = new ARDB.CurveArray())
        {
          curveArray.Append(projectedCurve.ToCurve());
          return view.Document.Create.NewSpaceBoundaryLines(sketchPlane, curveArray, view).get_Item(0);
        }
      }

      return default;
    }

    ARDB.ModelCurve Reconstruct(ARDB.ModelCurve spaceSeparator, ARDB.ViewPlan view, Curve curve)
    {
      if (!Reuse(spaceSeparator, view, curve))
        spaceSeparator = Create(view, curve);

      return spaceSeparator;
    }
  }
}
