using AMADotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AMADotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        private string _blogEndpoint = "https://localhost:7163/api/blog";
        private readonly RestClient client = new RestClient();
        public async Task Run()
        {
            //await Read();
            //await Edit(1);
            //await Edit(101);
            await Create("created title", "created Author", "Created Content");
            //await Update(37, "updated title new", "updated Author new", "Updated Content");
            //await Delete(300);
        }

        public async Task Read()
        {
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
            RestRequest request = new RestRequest(_blogEndpoint, Method.Post);
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        public async Task Update(int id, string title, string author, string content)
        {
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Put);
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Id = id,
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            request.AddJsonBody(blog);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

        public async Task Delete(int id)
        {
            RestRequest request = new RestRequest($"{_blogEndpoint}/{id}", Method.Delete);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content!);
        }

    }
}
