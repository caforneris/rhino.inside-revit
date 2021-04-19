using System.Collections.Generic;

namespace RhinoInside.Revit.External.DB.Schemas
{
  public partial class ParameterGroup
  {
    static readonly Dictionary<ParameterGroup, int> map = new Dictionary<ParameterGroup, int>()
    {
      { ElectricalAnalysis, -5000231 }, // PG_ELECTRICAL_ANALYSIS
      { AlternateUnits, -5000230 }, // PG_ALTERNATE_UNITS
      { PrimaryUnits, -5000229 }, // PG_PRIMARY_UNITS
      { WallCrossSection, -5000228 }, // PG_WALL_CROSS_SECTION
      { RouteAnalysis, -5000227 }, // PG_ROUTE_ANALYSIS
      { GeoLocation, -5000226 }, // PG_GEO_LOCATION
      { StructuralSectionGeometry, -5000225 }, // PG_STRUCTURAL_SECTION_GEOMETRY
      { EnergyAnalysisBldgConsMtlThermalProps, -5000221 }, // PG_ENERGY_ANALYSIS_BLDG_CONS_MTL_THERMAL_PROPS
      { EnergyAnalysisRoomSpaceData, -5000220 }, // PG_ENERGY_ANALYSIS_ROOM_SPACE_DATA
      { EnergyAnalysisBuildingData, -5000219 }, // PG_ENERGY_ANALYSIS_BUILDING_DATA
      { CouplerArray, -5000218 }, // PG_COUPLER_ARRAY
      { EnergyAnalysisAdvanced, -5000217 }, // PG_ENERGY_ANALYSIS_ADVANCED
      { ReleasesMemberForces, -5000216 }, // PG_RELEASES_MEMBER_FORCES
      { SecondaryEnd, -5000214 }, // PG_SECONDARY_END
      { PrimaryEnd, -5000213 }, // PG_PRIMARY_END
      { Moments, -5000212 }, // PG_MOMENTS
      { Forces, -5000211 }, // PG_FORCES
      { FabricationProductData, -5000210 }, // PG_FABRICATION_PRODUCT_DATA
      { Reference, -5000208 }, // PG_REFERENCE
      { GeometryPositioning, -5000207 }, // PG_GEOMETRY_POSITIONING
      { DivisionGeometry, -5000206 }, // PG_DIVISION_GEOMETRY
      { SegmentsFittings, -5000205 }, // PG_SEGMENTS_FITTINGS
      { ContinuousrailEndTopExtension, -5000204 }, // PG_CONTINUOUSRAIL_END_TOP_EXTENSION
      { ContinuousrailBeginBottomExtension, -5000203 }, // PG_CONTINUOUSRAIL_BEGIN_BOTTOM_EXTENSION
      { StairsWinders, -5000202 }, // PG_STAIRS_WINDERS
      { StairsSupports, -5000201 }, // PG_STAIRS_SUPPORTS
      { StairsOpenEndConnection, -5000200 }, // PG_STAIRS_OPEN_END_CONNECTION
      { RailingSystemSecondaryFamilyHandrails, -5000199 }, // PG_RAILING_SYSTEM_SECONDARY_FAMILY_HANDRAILS
      { Termination, -5000198 }, // PG_TERMINATION
      { StairsTreadsRisers, -5000197 }, // PG_STAIRS_TREADS_RISERS
      { StairsCalculatorRules, -5000196 }, // PG_STAIRS_CALCULATOR_RULES
      { SplitProfileDimensions, -5000195 }, // PG_SPLIT_PROFILE_DIMENSIONS
      { Length, -5000194 }, // PG_LENGTH
      { Nodes, -5000193 }, // PG_NODES
      { AnalyticalProperties, -5000192 }, // PG_ANALYTICAL_PROPERTIES
      { AnalyticalAlignment, -5000191 }, // PG_ANALYTICAL_ALIGNMENT
      { SystemtypeRisedrop, -5000190 }, // PG_SYSTEMTYPE_RISEDROP
      { Lining, -5000189 }, // PG_LINING
      { Insulation, -5000188 }, // PG_INSULATION
      { OverallLegend, -5000187 }, // PG_OVERALL_LEGEND
      { Visibility, -5000186 }, // PG_VISIBILITY
      { Support, -5000185 }, // PG_SUPPORT
      { RailingSystemSegmentVGrid, -5000184 }, // PG_RAILING_SYSTEM_SEGMENT_V_GRID
      { RailingSystemSegmentUGrid, -5000183 }, // PG_RAILING_SYSTEM_SEGMENT_U_GRID
      { RailingSystemSegmentPosts, -5000182 }, // PG_RAILING_SYSTEM_SEGMENT_POSTS
      { RailingSystemSegmentPatternRemainder, -5000181 }, // PG_RAILING_SYSTEM_SEGMENT_PATTERN_REMAINDER
      { RailingSystemSegmentPatternRepeat, -5000180 }, // PG_RAILING_SYSTEM_SEGMENT_PATTERN_REPEAT
      { RailingSystemFamilySegmentPattern, -5000179 }, // PG_RAILING_SYSTEM_FAMILY_SEGMENT_PATTERN
      { RailingSystemFamilyHandrails, -5000178 }, // PG_RAILING_SYSTEM_FAMILY_HANDRAILS
      { RailingSystemFamilyTopRail, -5000177 }, // PG_RAILING_SYSTEM_FAMILY_TOP_RAIL
      { ConceptualEnergyDataBuildingServices, -5000176 }, // PG_CONCEPTUAL_ENERGY_DATA_BUILDING_SERVICES
      { Data, -5000175 }, // PG_DATA
      { ElectricalCircuiting, -5000174 }, // PG_ELECTRICAL_CIRCUITING
      { General, -5000173 }, // PG_GENERAL
      { Flexible, -5000172 }, // PG_FLEXIBLE
      { EnergyAnalysisConceptualModel, -5000171 }, // PG_ENERGY_ANALYSIS_CONCEPTUAL_MODEL
      { EnergyAnalysisDetailedModel, -5000170 }, // PG_ENERGY_ANALYSIS_DETAILED_MODEL
      { EnergyAnalysisDetailedAndConceptualModels, -5000169 }, // PG_ENERGY_ANALYSIS_DETAILED_AND_CONCEPTUAL_MODELS
      { Fitting, -5000168 }, // PG_FITTING
      { ConceptualEnergyData, -5000167 }, // PG_CONCEPTUAL_ENERGY_DATA
      { Area, -5000166 }, // PG_AREA
      { AdskModelProperties, -5000165 }, // PG_ADSK_MODEL_PROPERTIES
      { CurtainGridV, -5000164 }, // PG_CURTAIN_GRID_V
      { CurtainGridU, -5000163 }, // PG_CURTAIN_GRID_U
      { Display, -5000162 }, // PG_DISPLAY
      { AnalysisResults, -5000161 }, // PG_ANALYSIS_RESULTS
      { SlabShapeEdit, -5000160 }, // PG_SLAB_SHAPE_EDIT
      { LightPhotometrics, -5000159 }, // PG_LIGHT_PHOTOMETRICS
      { PatternApplication, -5000158 }, // PG_PATTERN_APPLICATION
      { GreenBuilding, -5000157 }, // PG_GREEN_BUILDING
      { Profilen2, -5000156 }, // PG_PROFILE_2
      { Profilen1, -5000155 }, // PG_PROFILE_1
      { Profile, -5000154 }, // PG_PROFILE
      { TrussFamilyBottomChord, -5000153 }, // PG_TRUSS_FAMILY_BOTTOM_CHORD
      { TrussFamilyTopChord, -5000152 }, // PG_TRUSS_FAMILY_TOP_CHORD
      { TrussFamilyDiagWeb, -5000151 }, // PG_TRUSS_FAMILY_DIAG_WEB
      { TrussFamilyVertWeb, -5000150 }, // PG_TRUSS_FAMILY_VERT_WEB
      { Title, -5000149 }, // PG_TITLE
      { FireProtection, -5000148 }, // PG_FIRE_PROTECTION
      { RotationAbout, -5000147 }, // PG_ROTATION_ABOUT
      { TranslationIn, -5000146 }, // PG_TRANSLATION_IN
      { AnalyticalModel, -5000145 }, // PG_ANALYTICAL_MODEL
      { RebarArray, -5000144 }, // PG_REBAR_ARRAY
      { RebarSystemLayers, -5000143 }, // PG_REBAR_SYSTEM_LAYERS
      { CurtainGrid, -5000141 }, // PG_CURTAIN_GRID
      { CurtainMullionn2, -5000140 }, // PG_CURTAIN_MULLION_2
      { CurtainMullionHoriz, -5000139 }, // PG_CURTAIN_MULLION_HORIZ
      { CurtainMullionn1, -5000138 }, // PG_CURTAIN_MULLION_1
      { CurtainMullionVert, -5000137 }, // PG_CURTAIN_MULLION_VERT
      { CurtainGridn2, -5000136 }, // PG_CURTAIN_GRID_2
      { CurtainGridHoriz, -5000135 }, // PG_CURTAIN_GRID_HORIZ
      { CurtainGridn1, -5000134 }, // PG_CURTAIN_GRID_1
      { CurtainGridVert, -5000133 }, // PG_CURTAIN_GRID_VERT
      { Ifc, -5000131 }, // PG_IFC
      { Electrical, -5000130 }, // PG_ELECTRICAL
      { EnergyAnalysis, -5000129 }, // PG_ENERGY_ANALYSIS
      { StructuralAnalysis, -5000128 }, // PG_STRUCTURAL_ANALYSIS
      { MechanicalAirflow, -5000127 }, // PG_MECHANICAL_AIRFLOW
      { MechanicalLoads, -5000126 }, // PG_MECHANICAL_LOADS
      { ElectricalLoads, -5000125 }, // PG_ELECTRICAL_LOADS
      { ElectricalLighting, -5000124 }, // PG_ELECTRICAL_LIGHTING
      { Text, -5000123 }, // PG_TEXT
      { ViewCamera, -5000122 }, // PG_VIEW_CAMERA
      { ViewExtents, -5000121 }, // PG_VIEW_EXTENTS
      { Pattern, -5000120 }, // PG_PATTERN
      { Constraints, -5000119 }, // PG_CONSTRAINTS
      { Phasing, -5000114 }, // PG_PHASING
      { Mechanical, -5000113 }, // PG_MECHANICAL
      { Structural, -5000112 }, // PG_STRUCTURAL
      { Plumbing, -5000111 }, // PG_PLUMBING
      { ElectricalEngineering, -5000110 }, // PG_ELECTRICAL_ENGINEERING
      { StairStringers, -5000109 }, // PG_STAIR_STRINGERS
      { StairRisers, -5000108 }, // PG_STAIR_RISERS
      { StairTreads, -5000107 }, // PG_STAIR_TREADS
      { Underlay, -5000106 }, // PG_UNDERLAY
      { Materials, -5000105 }, // PG_MATERIALS
      { Graphics, -5000104 }, // PG_GRAPHICS
      { Construction, -5000103 }, // PG_CONSTRUCTION
      { Geometry, -5000101 }, // PG_GEOMETRY
      { IdentityData, -5000100 }, // PG_IDENTITY_DATA
    };
  }
}
