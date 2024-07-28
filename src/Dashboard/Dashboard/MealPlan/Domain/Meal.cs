namespace Dashboard.MealPlan.Domain;

public class Meal {
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public Meal(Guid uuid, string name, string description) {
        Uuid = uuid;
        Name = name;
        Description = description;
    }
}

public record MealDTOOut(Guid Uuid, string Name, string Description);
public record MealDTOIn(string Name, string Description);

public class MealMapper {
    public Meal Map(MealDTOOut mealDTO)
        => new(
            mealDTO.Uuid,
            mealDTO.Name,
            mealDTO.Description);

    public MealDTOOut Map(Meal meal)
        => new(
            meal.Uuid,
            meal.Name,
            meal.Description);

    public Meal Map(MealDTOIn mealDTO)
        => new(
            Guid.NewGuid(),
            mealDTO.Name,
            mealDTO.Description);
}
