namespace Dashboard.MealPlan.Domain;

public class MealPlan {
    public Guid Uuid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Meal> Meals { get; set; } = new();

    public MealPlan(Guid uuid, string name, string? description) {
        Uuid = uuid;
        Name = name;
        Description = description;
    }

    public MealPlan(Guid uuid, string name, string? description, List<Meal> meals) {
        Uuid = uuid;
        Name = name;
        Description = description;
        Meals = meals;
    }
}

public record MealPlanDTOOut(Guid Uuid, string Name, string? Description, List<Meal> Meals);
public record MealPlanDTOIn(string Name, string? Description, List<Meal> Meals);

public class MealPlanMapper {
    private readonly MealMapper mealMapper;

    public MealPlanMapper(MealMapper mealMapper) {
        this.mealMapper = mealMapper;
    }

    public MealPlan Map(MealPlanDTOOut mealPlanDTO)
        => new(
            mealPlanDTO.Uuid,
            mealPlanDTO.Name,
            mealPlanDTO.Description,
            mealPlanDTO.Meals);

    public MealPlanDTOOut Map(MealPlan mealPlan)
        => new(
            mealPlan.Uuid,
            mealPlan.Name,
            mealPlan.Description,
            mealPlan.Meals);

    public MealPlan Map(MealPlanDTOIn mealPlanDTO)
        => new(
            Guid.NewGuid(),
            mealPlanDTO.Name,
            mealPlanDTO.Description,
            mealPlanDTO.Meals);
}

