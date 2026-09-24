using System;

public class WageCalculator
{
    public static void Main()
    {
        int totalEmployees = 0;
        int skillLevel1Count = 0;
        int skillLevel2Count = 0;
        int skillLevel3Count = 0;
        double totalNetPay = 0;

        // Counters for each insurance type
        int medicalInsuranceCount = 0;
        int dentalInsuranceCount = 0;
        int longTermInsuranceCount = 0;

        // Retirement and deduction totals
        int retirementCount = 0;
        double totalRetirementAmount = 0;
        double totalInsuranceAmount = 0;

        while (true)
        {
            Console.Write("Enter your name (or 'exit' to finish): ");
            string userName = Console.ReadLine();
            if (userName.ToLower() == "exit")
                break;

            // Skill Level Dialogue
            Console.WriteLine("Skill Level    Hourly Wage");
            Console.WriteLine("1              17.00");
            Console.WriteLine("2              20.00");
            Console.WriteLine("3              22.00");

            Console.Write("Input your Skill Level (1, 2, or 3): ");
            string skillLevelInput = Console.ReadLine();
            if (string.IsNullOrEmpty(skillLevelInput))
                continue;

            char skillLevelChar = skillLevelInput[0];
            double hourlyWage;

            switch (skillLevelChar)
            {
                case '1':
                    hourlyWage = 17.0;
                    skillLevel1Count++;
                    break;
                case '2':
                    hourlyWage = 20.0;
                    skillLevel2Count++;
                    break;
                case '3':
                    hourlyWage = 22.0;
                    skillLevel3Count++;
                    break;
                default:
                    Console.WriteLine("Invalid Skill Level. Please choose 1, 2, or 3.");
                    continue;
            }

            Console.Write("Number of Hours Worked: ");
            if (!double.TryParse(Console.ReadLine(), out double hoursWorked))
            {
                Console.WriteLine("Invalid input for hours worked. Please enter a number.");
                continue;
            }

            double regularPay = hourlyWage * Math.min(hoursWorked, 40);
            double overtimePay = hoursWorked > 40 ? (hoursWorked - 40) * (hourlyWage * 1.5) : 0;
            double grossPay = regularPay + overtimePay;

            double totalDeductions = 0;
            Console.Write("Do you want insurance? (yes/no): ");
            if (Console.ReadLine().ToLower() == "yes")
            {
                Console.WriteLine("Insurance Type    Weekly Cost");
                Console.WriteLine("1 - Medical Insurance - $32.00");
                Console.WriteLine("2 - Dental Insurance - $28.00");
                Console.WriteLine("3 - Long Term Disability Insurance - $10.00");

                Console.Write("Choose an insurance type (1, 2, or 3): ");
                string insuranceChoice = Console.ReadLine();
                switch (insuranceChoice)
                {
                    case "1":
                        totalDeductions += 32.00;
                        medicalInsuranceCount++;
                        totalInsuranceAmount += 32.00;
                        break;
                    case "2":
                        totalDeductions += 28.00;
                        dentalInsuranceCount++;
                        totalInsuranceAmount += 28.00;
                        break;
                    case "3":
                        totalDeductions += 10.00;
                        longTermInsuranceCount++;
                        totalInsuranceAmount += 10.00;
                        break;
                    default:
                        Console.WriteLine("Invalid insurance choice.");
                        continue;
                }
            }

            double retirementContribution = 0;
            if (skillLevelChar == '3')
            {
                Console.Write("Do you want to participate in the retirement plan? (yes/no): ");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    retirementContribution = grossPay * 0.03;
                    totalDeductions += retirementContribution;
                    retirementCount++;
                    totalRetirementAmount += retirementContribution;
                }
            }

            if (totalDeductions > grossPay)
            {
                Console.WriteLine("Total deductions exceed gross pay. Please check your inputs.");
                continue;
            }

            double netPay = grossPay - totalDeductions;

            // Display Summary
            Console.WriteLine($"Summary for {userName}:");
            Console.WriteLine($"Hours Worked: {hoursWorked}");
            Console.WriteLine($"Hourly Pay Rate: ${hourlyWage:F2}");
            Console.WriteLine($"Regular Pay: ${regularPay:F2}");
            Console.WriteLine($"Overtime Pay: ${overtimePay:F2}");
            Console.WriteLine($"Gross Pay: ${grossPay:F2}");
            Console.WriteLine($"Total Deductions: ${totalDeductions:F2}");
            Console.WriteLine($"Net Pay: ${netPay:F2}");

            totalEmployees++;
            totalNetPay += netPay;
        }

        // Final Summary
        Console.WriteLine($"Total Employees Processed: {totalEmployees}");
        Console.WriteLine($"Employees by Skill Level:");
        Console.WriteLine($"Skill Level 1: {skillLevel1Count}");
        Console.WriteLine($"Skill Level 2: {skillLevel2Count}");
        Console.WriteLine($"Skill Level 3: {skillLevel3Count}");
        Console.WriteLine($"Grand Total Net Pay: ${totalNetPay:F2}");

        // Show breakdown of insurance types
        Console.WriteLine($"Total Employees with Insurance:");
        Console.WriteLine($"Medical Insurance: {medicalInsuranceCount}");
        Console.WriteLine($"Dental Insurance: {dentalInsuranceCount}");
        Console.WriteLine($"Long Term Disability Insurance: {longTermInsuranceCount}");
        Console.WriteLine($"Total Insurance Amount: ${totalInsuranceAmount:F2}");

        // Retirement summary
        Console.WriteLine($"Total Employees in Retirement Plan: {retirementCount}, Total Retirement Amount: ${totalRetirementAmount:F2}");
    }
}
