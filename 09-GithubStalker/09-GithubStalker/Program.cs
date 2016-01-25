using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;





namespace _09_GithubStalker
{
    class Program
    {
        static void Main(string[] args)
        {
           
            string username = "";

            while (true)
            {
            
                try
                {
                    Console.WriteLine("Enter a GitHub username");
                    username = Console.ReadLine();

                    WebClient wc = new WebClient();

                        wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        string json = wc.DownloadString("https://api.github.com/users/" + username);
                        var o = JObject.Parse(json);

                        Console.WriteLine("Name: " + o["name"].ToString());
                        Console.WriteLine("URL: " + o["url"].ToString());
                        Console.WriteLine("Number of Followers: " + o["followers"].ToString());
                        Console.WriteLine("");
                        Console.WriteLine("Number of Repositories: " + o["public_repos"].ToString());


                    WebClient wcRepo = new WebClient();
         
                        wcRepo.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        String jsonRepo = wcRepo.DownloadString("https://api.github.com/users/" + username + "/repos");
                        var objectRepo = JArray.Parse(jsonRepo);

                        foreach (var repo in objectRepo)
                        {
                            Console.WriteLine("------" + repo["name"] + " " + repo["stargazers_count"] + " stars, " + repo["watchers_count"] + " watchers");
                        }
       
                    break;
                }


                catch
                {
                    Console.WriteLine("That is not a valid GitHub username. ");
                    Console.WriteLine();
                }
            }



           

            while (true)
            {
                
                

                try
                {
                    Console.WriteLine("To view number of commits and issues, enter project name: ");
                    string projectCommit = Console.ReadLine();
                    WebClient commitRepo = new WebClient();
                    {
                        commitRepo.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        String jsonCommit = commitRepo.DownloadString("https://api.github.com/repos/" + username + "/" + projectCommit + "/commits");
                        var objectCommit = JArray.Parse(jsonCommit);
                        Console.WriteLine("Number of commits: " + objectCommit.Count.ToString());
                    }

                    WebClient issueRepo = new WebClient();
                    {
                        issueRepo.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        String jsonIssue = issueRepo.DownloadString("https://api.github.com/repos/" + username + "/" + projectCommit + "/issues");
                        var objectIssue = JArray.Parse(jsonIssue);
                        Console.WriteLine("Number of issues: " + objectIssue.Count.ToString());
                        Console.ReadLine();
                        break;
                    }





                   
                }
                catch
                {
                    Console.WriteLine("That is not a valid project name. ");

                }
                
            }
            

        }
    }
}
