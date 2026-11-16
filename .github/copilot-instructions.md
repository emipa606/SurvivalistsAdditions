# GitHub Copilot Instructions for Survivalist's Additions (Continued) Mod

## Mod Overview and Purpose

Survivalist's Additions (Continued) is a RimWorld mod that updates and expands upon the features of the cuproPandas mod, originally developed by mantrasong. The mod focuses on enhancing the primitive and tribal playthrough experience by introducing new items, objects, and features. The mod replaces the Universal Fermenter with the Processor Framework, ensuring compatibility and leveraging new functionalities. Key features include the addition of food items such as charcoal, grilled meals, cheese, and smoked meat, along with new plants like turnips and cabbage that can be grown in colder temperatures. Additionally, the mod includes various tools and apparel items like the shemagh and burlap fabric.

## Key Features and Systems

- **Items Added**: Charcoal, Grilled Meals, Curdled Milk, Cheese, Smoked Meat, Smoked Cheese, Turnip, Cabbage, Prickly Pear, Turnip Salad, Cabbage Salad, Vinegar, Pickled Vegetables, Shemagh, Jute Fiber, Burlap, Rimshroom.
- **Objects Added**: Grill, Shelter Door, Shelter Wall, Shelter Heater, Charcoal Pit, Smoker, Forge, Bellows, Vinegar Barrel, Cheese Barrel, Camp Table, Camp Butcher Table, Snare.
- **Hardy Plants**: Turnips and Cabbage can grow in colder temperatures.
- **New Jobs**: JobDriver_DisableSnare and ThoughtWorker_WearingBurlapApparel.
- **Incidents**: IncidentWorker_MushroomSprout for spawning mushrooms.
- **XML Definitions**: Various definitions for items, jobs, incidents, and more.

## Coding Patterns and Conventions

### General Conventions

- **C# Naming Conventions**: Follow standard C# naming conventions for classes, methods, and variables. Use PascalCase for class and method names, and camelCase for variable names.
- **XML Naming Conventions**: Use lowercase for XML element and attribute names. Ensure consistency in naming conventions across XML files.
- **Code Comments**: Include detailed comments for complex logic or critical sections of code. Comments should explain the purpose and any side effects of the code.

### C# Code Patterns

- **Mod Structure**: Organize code into logical namespaces and classes. For example, AI-related code should be in the `AI` namespace, while map-related code should be in the `Map` namespace.
- **Encapsulation**: Use encapsulation principles to protect internal state and provide public interfaces for interacting with classes. Use properties and methods to expose functionality.
- **Error Handling**: Implement error handling mechanisms to manage exceptions and unexpected behavior. Use try-catch blocks and appropriate logging to capture errors.
- **Harmony Patches**: Use Harmony for patching existing RimWorld methods. Ensure that patches are well-documented and maintain compatibility with other mods.

### XML Code Patterns

- **Def Files**: Use def files to define new items, objects, and other game elements. Ensure that def files are well-structured and follow the conventions of existing RimWorld def files.
- **Def Types**: Define new def types as needed, ensuring that they integrate seamlessly with existing game mechanics.
- **Def References**: Use references to existing defs where appropriate to avoid duplication and maintain consistency.

## XML Integration

### Def File Structure

- **HediffDefs**: Define new hediff effects.
- **IncidentDefs**: Define new incidents and their triggers.
- **ThingCategories**: Define new categories for items and objects.
- **JobDefs**: Define new jobs and their associated logic.
- **ThingDefs_Items**: Define new items, including equipment and apparel.

### Example Def File

xml
<HediffDef>
    <defName>WearingBurlapApparel</defName>
    <label>Wearing Burlap Apparel</label>
    <description>Wearing burlap apparel affects mood negatively.</description>
    <stages>
        <li>
            <label>Wearing Burlap Apparel</label>
            <severity>0.5</severity>
            <statOffsets>
                <li>
                    <stat>Mood</stat>
                    <value>-1</value>
                </li>
            </statOffsets>
        </li>
    </stages>
</HediffDef>


### XML Patches

- **Patching Existing Defs**: Use XML patches to modify existing defs without altering the original RimWorld files. Ensure that patches are applied correctly and do not interfere with other mods.
- **Patching Existing Methods**: Use Harmony patches to modify existing methods in the RimWorld codebase. Ensure that patches are well-documented and maintain compatibility with other mods.

## Harmony Patching

### Example Harmony Patch

csharp
using HarmonyLib;
using RimWorld;
using Verse;

namespace SurvivalistsAdditions.AI
{
    public static class JobDriver_DisableSnare_Patches
    {
        [HarmonyPatch(typeof(JobDriver_DisableSnare), "MakeNewToils")]
        public static void Postfix(JobDriver_DisableSnare __instance)
        {
            // Custom logic for making new toils
        }
    }
}


### Patching Considerations

- **Method Signature**: Ensure that the method signature matches the original method being patched.
- **Patch Type**: Use appropriate patch types (e.g., Prefix, Postfix, Transpiler) based on the desired behavior.
- **Compatibility**: Ensure that patches do not interfere with other mods or the base game. Test patches thoroughly before releasing.

## Suggestions for Copilot

- **Contextual Code Generation**: Use Copilot to generate code snippets that integrate with the existing mod structure and conventions.
- **XML Def Generation**: Use Copilot to generate XML def files based on existing patterns and conventions.
- **Harmony Patch Generation**: Use Copilot to generate Harmony patches for modifying existing RimWorld methods.
- **Code Completion**: Use Copilot to complete code snippets and reduce the amount of boilerplate code.
- **Documentation Generation**: Use Copilot to generate documentation for new classes, methods, and def files.

## Contributing Guidelines

- **Code Style**: Follow the coding patterns and conventions outlined above.
- **Testing**: Ensure that new features and patches are thoroughly tested in a RimWorld game.
- **Documentation**: Update the mod documentation to reflect new features and changes.
- **Compatibility**: Maintain compatibility with other mods and the base game.

By following these guidelines, developers can contribute effectively to the Survivalist's Additions (Continued) mod, ensuring that new features are well-integrated and maintain compatibility with other mods and the base game.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

