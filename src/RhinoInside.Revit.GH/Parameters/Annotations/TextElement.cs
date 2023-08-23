using System;
using System.Windows.Forms;
using Grasshopper.Kernel;
using ARDB = Autodesk.Revit.DB;

namespace RhinoInside.Revit.GH.Parameters
{
  public class TextElement : GraphicalElement<Types.TextElement, ARDB.TextElement>
  {
    public override GH_Exposure Exposure => GH_Exposure.quinary | GH_Exposure.obscure;
    public override Guid ComponentGuid => new Guid("E2435930-2F95-4277-BA10-B1E3A660F9DA");

    public TextElement() : base
    (
      name: "Text Note",
      nickname: "TxtNote",
      description: "Contains a collection of Revit text note elements",
      category: "Params",
      subcategory: "Revit Elements"
    )
    { }

    #region UI
    protected override void Menu_AppendPromptNew(ToolStripDropDown menu)
    {
      Menu_AppendPromptNew(menu, Autodesk.Revit.UI.PostableCommand.Text);
    }
    #endregion
  }
}