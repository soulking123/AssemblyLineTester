using System;
using System.Linq;
using AssemblyLineTester.Data;
using AssemblyLineTester.Models;
using Microsoft.EntityFrameworkCore;

namespace AssemblyLineTester.Services
{
    public class TestConfigService
    {
        public void SaveConfig(TestConfiguration config)
        {
            using (var db = new AppDbContext())
            {
                // Check if the program already exists by name
                var existingProgram = db.TestConfigurations
                    .Include(c => c.Nets)
                    .FirstOrDefault(c => c.ConfigName == config.ConfigName);

                if (existingProgram != null)
                {
                    // A. UPDATE: Overwrite details of the existing program
                    existingProgram.ProductModel = config.ProductModel;
                    existingProgram.Description = config.Description;
                    existingProgram.UpdatedAt = DateTime.UtcNow;

                    // Remove old detail pin rows, then add the new ones
                    db.TestConfigNets.RemoveRange(existingProgram.Nets);
                    existingProgram.Nets = config.Nets;
                }
                else
                {
                    // B. INSERT: Save as a brand new program entry
                    db.TestConfigurations.Add(config);
                }

                db.SaveChanges();
            }
        }

        public void SetActiveConfig(string configName)
        {
            using (var db = new AppDbContext())
            {
                // Get all programs in the database
                var allPrograms = db.TestConfigurations.ToList();
                // Loop through and toggle active state
                foreach (var p in allPrograms)
                {
                    if (p.ConfigName == configName)
                    {
                        p.IsActive = true;
                    }
                    else
                    {
                        p.IsActive = false;
                    }
                }
                db.SaveChanges();
            }
        }

        public TestConfiguration? GetActiveConfig()
        {
            using (var db = new AppDbContext())
            {
                return db.TestConfigurations
                    .Include(c => c.Nets)
                    .FirstOrDefault(c => c.IsActive);
            }
        }

        public string ConvertToProgramText(TestConfiguration config)
        {
            var sb = new System.Text.StringBuilder();

            // 1. Build Header
            sb.AppendLine("* ");
            sb.AppendLine();
            sb.AppendLine("* CRC Signature: Z9S34P");
            sb.AppendLine();

            string programName = !string.IsNullOrWhiteSpace(config.ConfigName) ? config.ConfigName : "PROGRAM";
            sb.AppendLine($"({programName}");
            sb.AppendLine();
            sb.AppendLine(" J1 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine(" J3 D507F1 *[AHED-64,0,0,0,0,0,0,0,0,64] 64 POS. .1\" FEM.");
            sb.AppendLine();

            // 2. Set Resistance Limits dynamically based on nets
            double connResist = 5;
            var resNet = config.Nets.FirstOrDefault(n => n.UpperLimit.HasValue);
            if (resNet != null && resNet.UpperLimit.HasValue)
            {
                connResist = resNet.UpperLimit.Value;
            }

            sb.AppendLine($"CONNECTION RESIS  {connResist} ohm");
            sb.AppendLine("LV INSULATION RESIS  100 K ohm");
            sb.AppendLine("HIPOT VOLTAGE  OFF");
            sb.AppendLine("** TEST METHOD CONTINUOUS");
            sb.AppendLine();

            // 3. Populate Net list connection paths
            foreach (var net in config.Nets.OrderBy(n => n.SequenceNo))
            {
                string fromPin = FormatPin(net.FromAddress);
                string toPin = FormatPin(net.ToAddress);

                sb.AppendLine($"{net.SequenceNo}   {fromPin}  {toPin}  ");
            }

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine(")");

            return sb.ToString();
        }

        private string FormatPin(int address)
        {
            if (address < 64)
            {
                return $"J1-{address:D3}";
            }
            else
            {
                return $"J3-{(address - 64):D3}";
            }
        }
    }
}