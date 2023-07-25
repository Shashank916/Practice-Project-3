using System;
using System.Collections.Generic;
using System.IO;

namespace StudentDataConsoleApp
{
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }

        public Student(string name, string className)
        {
            Name = name;
            Class = className;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "D:\\PracticeProject-3\\Practice Project\\student_data.txt");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("The file 'students.txt' does not exist. Please create the file with student data and try again.");
                Console.ReadLine();
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 2) 
                    {
                        string name = parts[0].Trim();
                        string className = parts[1].Trim();
                        Student student = new Student(name, className);
                        students.Add(student);
                    }
                    else
                    {
                        Console.WriteLine($"Skipping invalid line: {line}");
                    }
                }
                students.Sort((s1, s2) => s1.Name.CompareTo(s2.Name));
                Console.Write("Enter the name to search for: ");
                string searchName = Console.ReadLine().Trim().ToLower(); 
                List<Student> searchResults = students.FindAll(s => s.Name.ToLower().Contains(searchName));
                Console.WriteLine("\nSearch Results:");
                if (searchResults.Count > 0)
                {
                    foreach (Student student in searchResults)
                    {
                        Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                    }
                }
                else
                {
                    Console.WriteLine("No matching student found.");
                }
                Console.WriteLine("\nSorted Student Data:");
                foreach (Student student in students)
                {
                    Console.WriteLine($"Name: {student.Name}, Class: {student.Class}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine(); 
        }
    }
}
