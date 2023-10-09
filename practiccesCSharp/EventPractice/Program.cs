
using EventPractice;

ProccessBusinessLogic proccessBusinessLogic = new ProccessBusinessLogic();



proccessBusinessLogic.ProccesCompleted += ProccessBusinessLogic_ProccesCompleted;
proccessBusinessLogic.ProccesFailed += ProccessBusinessLogic_ProccesFailed;
proccessBusinessLogic.StartProcces("Hello");
void ProccessBusinessLogic_ProccesCompleted()
{
    Console.WriteLine("Procces Completed");
}



proccessBusinessLogic.ProccesFailed += ProccessBusinessLogic_ProccesFailed;


proccessBusinessLogic.ProcessError("Sorry I cant handle the request because Your logic is incorrect");

void ProccessBusinessLogic_ProccesFailed(string message)
{
    Console.WriteLine(message);
}



Console.WriteLine("---Event using built in event handler-------");

proccessBusinessLogic.ProccesReceived += bl_ProcessCompleted; // register with an event
proccessBusinessLogic.ProccessStart();


// event handler
static void bl_ProcessCompleted(object sender, ProccessEventArgs e)
{
    Console.WriteLine("Process " + (e.IsSuccessful ? "Completed Successfully" : "failed"));
    Console.WriteLine("Completion Time: " + e.CompletionTime.ToLongDateString());
}


Console.WriteLine("-----EmailSending Result----");


proccessBusinessLogic.OnEmailSent += ProccessBusinessLogic_OnEmailSent;

proccessBusinessLogic.EmailSent();

void ProccessBusinessLogic_OnEmailSent(object? sender, EmailEventArgs e)
{
    if (e.IsSuccessful) Console.WriteLine("Completed");
    else Console.WriteLine("Failed");
    Console.WriteLine($"completion time : {e.CompletionTime.ToLongTimeString()} on {e.CompletionTime.ToLongDateString()}");
}