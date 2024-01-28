using AMADotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMADotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private string _blogEndpoint = "https://localhost:7163/api/blog";
        public async Task Run()
        {
            await Read();
            await Edit(1);
            await Edit(101);
            await Create("created title", "created Author", "Created Content");
        }

        public async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint,Method.Get);
            //await client.GetAsync(request);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonString)!;
                foreach (BlogDataModel item in lst)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
            }
        }

        public async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Get);
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content!;
                BlogDataModel item = JsonConvert.DeserializeObject<BlogDataModel>(jsonString)!;
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            else
            {
                Console.WriteLine(response.Content!);
            }
        }
        public async Task Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            RestClient client = new RestClient();
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            string jsonString = response.Content!;
        }

        public void Update()
        {

        }

        public void Delete()
        {

        }
    }
}
