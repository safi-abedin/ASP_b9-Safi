using EventPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventPractice
{
    public class ProccessBusinessLogic
    {
        public event Action ProccesCompleted;

        public event Action<string> ProccesFailed;


        public event EventHandler<ProccessEventArgs> ProccesReceived;


        public event EventHandler<EmailEventArgs> OnEmailSent;


       // first you  need to create a event then if you want to invoke after execution then write in the method
       // then go to proramm.cs and create object then with with the bind the event and subscribe with new method and write that method
       // when the object will call the method from class the event will fire and program .cs subscribe  method  will execute.


        public void StartProcces()
        {
            Console.WriteLine("Proccess Started");

            Console.WriteLine
                (
                """
                Here the logic will be written ---> when the logic operation is finishd 
                then the proccessCompleted event will fire 
                as we subcribe the the event in this method 
                """
                );

            ProccesCompleted?.Invoke();
        }


        public void ProcessError(string message)
        {
            Console.WriteLine("The proccess Started to operate your logic");

            ProccesFailed?.Invoke(message);
        }

        public void ProccessStart() 
        { 
        var data = new ProccessEventArgs();
		
        try
        {
            Console.WriteLine("Process Started!");
			
            // some code here..
            
            data.IsSuccessful = true;
            data.CompletionTime = DateTime.Now;
            OnProcessCompleted(data);
        }
        catch(Exception)
        {
            data.IsSuccessful = false;
            data.CompletionTime = DateTime.Now;
             OnProcessCompleted(data);
        }
        }
        protected virtual void OnProcessCompleted(ProccessEventArgs e)
        {
            ProccesReceived?.Invoke(this, e);
        }



        public void EmailSent()
        {
            var data = new EmailEventArgs();

            try
            {
                Console.WriteLine("Email Sending");

                //Proccess Of Email Sending

                data.IsSuccessful = true;
                data.CompletionTime = DateTime.Now;
                OnEmailSent?.Invoke(this,data);

            }
            catch (Exception )
            {
                data.IsSuccessful= false;
                data.CompletionTime = DateTime.Now;
                OnEmailSent?.Invoke(this,data);
            }
        }
      

       
    }
}
