# Assignment #1: A SOLID Start — Recipe Manager
---

## Goals
Become familiar with and gain hands-on experience with SOLID principles and some design patterns.

## Task
In this assignment, we will create a simple Recipe Manager system. Since our goal is to work with architectural challenges rather than framework-specific ones, our system will be local (running on a single machine) and command-line based. No frameworks or external libraries are allowed in this task (if you need one, write your own).

The task will be divided into several parts:

#### 1. Basic Structure
Create a command-line program that supports the following commands:

Manage pantry stock:
```
stock add "<ingredient>" <quantity> <unit>
stock use "<ingredient>" <quantity> <unit> [<reason>]
stock info
```
Description: Add items to pantry, consume them, or view stock list.

Manage recipes:
```
recipe add "<recipe_name>"
recipe add-ingredient "<recipe_name>" "<ingredient>" <quantity> <unit>
recipe steps add "<recipe_name>" "<step_text>"
recipe info "<recipe_name>"
recipe list
```
Description: Create recipes with ingredients and steps.

Plan cooking and shopping:
```
plan add "<recipe_name>" <date> [<servings_multiplier>]
plan list
shopping export
```
Description: Plan recipes for dates and export shopping list (naive, not pantry-aware yet).

Note: At this point, no persistence is required. All data is lost when the program finishes. Next steps will add saving, so design the system now to make persistence easy later. Also, don't forget to separate commands execution from command line parsing.

#### 2. Extra Operations
Add an options command to show available actions for a specified recipe.

View available actions for a recipe:
```
options "<recipe_name>"
```
Actions available:
- info: View recipe details.
- scale <factor>: Preview scaled ingredient quantities.
- missing: List ingredients missing from pantry.
- cook [<servings_multiplier>]: Consume ingredients from pantry to cook the recipe.

Invoke an Action:
```
action <action_name> "<recipe_name>" [<parameters>]
```

Error Handling:
- Show error if action does not exist or recipe not found.
- Show clear error if pantry is insufficient when cooking.

#### 3. Multiple Users
Rework the program to require a login command first.

```
login <username>
```
Description: Logs in as user, creating profile if it doesn’t exist. Each user has isolated pantry, recipes, and plans.

#### 4. Different Plans with Limits
Introduce two plans: Basic and Chef.

Basic Plan
- Maximum of 15 pantry items.
- Maximum of 10 recipes.
- Maximum of 30 planned entries.
- Error message if user tries to exceed limits.

Chef Plan
- Maximum of 200 pantry items.
- Maximum of 150 recipes.
- Maximum of 500 planned entries.

Change Plan Command:
```
change_plan <plan_name>
```
Downgrade Constraints:
If user exceeds Basic limits, error must be shown when trying to downgrade.

#### 5. Session Saving
Implement persistence so profiles, pantry, recipes, and plans persist between sessions.

Requirements:
- Must work offline with only .NET installed, no extra libraries or installed software.

#### 6. Analyze It!
Log user actions to a file as JSON lines.

Event Log Format:
```
{ "event": "<event_name>", "timestamp": "<time>", "params": { ... } }
```

Events to Collect:

| Event Name           | Parameters                                        |
|----------------------|---------------------------------------------------|
| `user_logged_in`     | `user_name`                                       |
| `stock_added`        | `ingredient`, `quantity`, `unit`                  |
| `stock_used`         | `ingredient`, `quantity`, `unit`, `reason`        |
| `recipe_created`     | `recipe_name`                                     |
| `recipe_ingredient`  | `recipe_name`, `ingredient`, `quantity`, `unit`   |
| `recipe_cooked`      | `recipe_name`, `servings`                         |
| `planned_added`      | `recipe_name`, `date`, `servings`                 |
| `shopping_exported`  | `items_count`                                     |
| `plan_changed`       | `user_name`, `plan_name`                          |
| `limit_reached`      | `limit_type` (`pantry_items`/`recipes`/`plans`)   |

Example Event Entry:
```
{ "event": "recipe_cooked", "timestamp": "2025-10-02T18:22:04Z", "params": { "recipe_name": "Borscht", "servings": 4 } }
```


#### 7&8. Extra Features
As in real systems, new features will be added later. Leave at least one week free for this.

### Grading policy
Maximum points: 8
- 1 point total for steps 1-3;
- 1 point for steps 4-8
- 2 points for theoretical (and practical) questions on SOLID (can be gained only offline). Theoretical questions can be requested only when there are at least 4 completed parts in the repository. Questions are team-based, so the whole team must come to the practice session to answer.

### Deadlines
You must deliver MVP with steps 1-3 no later than October 1 23:59. Soft Deadline for assignment is October 12, 23:59. Hard deadline (50% penalty) is October 19, 23:59
