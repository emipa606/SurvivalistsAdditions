# GitHub Copilot Instructions for Survivalist's Additions (Continued) Mod Project

## Mod Overview and Purpose
**Survivalist's Additions (Continued)** is an extended and updated mod designed to enhance primitive and tribal-themed playthroughs in RimWorld. This mod offers a variety of new items, recipes, and objects that add depth and challenge by integrating more survivalist and crafting elements into the game. It is primarily focused on adding resources and crafting options emblematic of early technology levels.

## Key Features and Systems

### Items
- **Charcoal**: Used as fuel for grills, smokers, and forges.
- **Grilled Meals**: Enhance colonist mood with varieties like Meat, Veg, and Fine.
- **Curdled Milk & Cheese**: Dairy options with preservation advantages.
- **Smoked Foods**: Smoked meat and cheese extend shelf-life.
- **Turnips & Cabbage**: Hardy crops with extended growth capabilities.
- **Other Foods**: Includes Vinegar, Pickled Vegetables, and other salads.
- **Wearables**: Features like the Shemagh and Burlap that influence colonist comfort.

### Objects
- **Crafting Stations**: Include the Charcoal Pit, Smoker, Forge, and Cheese Barrel.
- **Utilities**: Such as the Shelter Door, Wall, and Heater suited for quick needs.
- **Hunting**: Automated Snare for small animal trapping.

### Compatibility
- Integrates seamlessly with other mods like Medieval Times, SeedsPlease, Zen Garden, and more.

## Coding Patterns and Conventions

### C# File Structure
- **Job Drivers**: Classes such as `JobDriver_DisableSnare` manage tasks associated with mod features.
- **Custom Thoughts**: A class like `ThoughtWorker_WearingBurlapApparel` manages mood impacts.
- **Building Extensions**: `Building_Snare`, for example, is an extension for traps that automates hunting functions.

### Naming Conventions
- Use PascalCase for class and method names.
- Use descriptive naming to clearly indicate the role and functionality of classes and methods.

## XML Integration
All items, recipes, and building definitions are integrated through XML files. Ensure that the XML files are correctly defined in the `Defs` folder for item, thing, and recipe definitions. This allows the seamless addition of new objects without altering the game's core files directly.

## Harmony Patching
Use Harmony for patching original game functions where necessary. Classes like `PatchOperationModDependent` are utilized for conditional logic applying patches dependent on the availability of certain mods. Make sure to structure patches to avoid conflicts and redundancy.

## Suggestions for Copilot Use

- **Write Efficient Code**: Use Copilot to generate boilerplate code for new items and buildings to ensure alignment with mod conventions.
- **XML Assistance**: Leverage Copilot to autocomplete XML definitions, ensuring proper attribute closure and tag nesting.
- **Harmony Patches**: Let Copilot assist in creating patch methods with correct syntax, especially for conditional logic and list layering.
- **Reusability**: Promote method reusability by prompting Copilot to suggest patterns and refactor common operations shared across multiple files or classes.
- **Documentation**: Utilize Copilot to generate docstrings and explanatory comments where functions are non-obvious or complex.

These instructions should serve as a guide for maintaining consistency and effectiveness when using GitHub Copilot in developing and expanding upon the Survivalist's Additions mod.
