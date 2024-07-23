public abstract class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public abstract void Subscribe(Student std);
}

public interface ICourseSubscription
{
    void Subscribe(Student std, Course course);
}

public class OnlineCourseSubscription : ICourseSubscription
{
    public void Subscribe(Student std, Course course)
    {
    }
}

public class OfflineCourseSubscription : ICourseSubscription
{
    public void Subscribe(Student std, Course course)
    {
        Console.WriteLine($"Student {std.Name} subscribed to hybrid course {"C# basico"}");
    }
}

public class OnlineCourse : Course
{
    public override void Subscribe(Student std)
    {
        var strategy = new OnlineCourseSubscription();
        strategy.Subscribe(std, this);
    }
}

public class OfflineCourse : Course
{
    public override void Subscribe(Student std)
    {
        var strategy = new OfflineCourseSubscription();
        strategy.Subscribe(std, this);
    }
}

public class HybridCourse : Course
{
    private ICourseSubscription onlineStrategy;
    private ICourseSubscription offlineStrategy;

    public HybridCourse(ICourseSubscription onlineSubscription, ICourseSubscription offlineSubscription)
    {
        this.onlineStrategy = onlineSubscription;
        this.offlineStrategy = offlineSubscription;
    }

    public override void Subscribe(Student std)
    {
        onlineStrategy.Subscribe(std, this);
        offlineStrategy.Subscribe(std, this);
    }
}

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        Student student = new Student { StudentId = 1, Name = "Eric Jimenez" };

        ICourseSubscription onlineSubscription = new OnlineCourseSubscription();
        ICourseSubscription offlineSubscription = new OfflineCourseSubscription();

        Course onlineCourse = new OnlineCourse { CourseId = 111, Title = "C# basico" };
        Course offlineCourse = new OfflineCourse { CourseId = 222, Title = "C# intermedio" };
        Course hybridCourse = new HybridCourse(onlineSubscription, offlineSubscription) { CourseId = 333, Title = "C# Avanzado" };

        onlineCourse.Subscribe(student);
        offlineCourse.Subscribe(student);
        hybridCourse.Subscribe(student);
    }
}

/*
Explicación:

Antes:
- Las clases OnlineCourse y OfflineCourse contenían la lógica para suscribirse a cursos específicos.
- No había una separación clara de responsabilidades.
- Para modificar o añadir nuevas estrategias de suscripción, era necesario modificar las clases existentes, lo que violaba el principio OCP.

Después:
- Implementamos una interfaz ICourseSubscription para definir estrategias de suscripción.
- Creamos clases específicas para las estrategias de suscripción: OnlineCourseSubscription y OfflineCourseSubscription.
- Las clases OnlineCourse y OfflineCourse ahora toman una estrategia de suscripción en su constructor y la utilizan en su método Subscribe, siguiendo el principio OCP.
- Implementamos la clase HybridCourse que toma dos estrategias de suscripción en su constructor y las utiliza en su método Subscribe.
- Ahora, si necesitamos añadir nuevas estrategias de suscripción, podemos hacerlo sin modificar las clases existentes, adhiriéndose al principio OCP.
*/
