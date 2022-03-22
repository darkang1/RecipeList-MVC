# Recipe Planner (MVC)

## Description
- Simple **Recipe Planner** application developed using ASP.NET MVC, C# based on .NET 5
- Data stored in **JSON** format

## Requirements
- Specify full path to your **recipes.json** file in **config.json** file.
##

### Main Screen
- Displays list of all available recipes
- Has buttons to **Add/Edit/Delete** recipes
- Has **Menu** option to generate a shopping list for specified recipe(s)

### Entry Dialog
- User may **Add** or **Edit** existing recipe
- Recipe consist of multiline description and list of ingredients
- Each ingredient has **name**, **quantity** and **unit** where:
  - **Name** is a multiword string (like **green pepper**)
  - **Quantity** is a float number (like **5.0**)
  - **Unit** is a multiword string (any string is treated as a unit - so "**fl oz**" is also a unit)
  
### Menu Dialog
- User may create a shopping list for specified recipe(s) to be cooked
- Application will compute sorted list of required ingredients to buy
- Only compatible (equal strings) units are summed up (see **eggs** with **pcs**)
  - If units are not compatible ingredients are presented separately (see **salt**)

## Data Structure
- All recipes are stored in **recipes.json**
