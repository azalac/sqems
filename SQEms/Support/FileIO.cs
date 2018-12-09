﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Support
{
    public delegate string ProdecureGenerator(int aptid, int procid);

    public static class FileIO
    {
        public static void WriteAllBillableProcedures(string path,
            DatabaseTable appointment, DatabaseTable procedures,
            int month, ProdecureGenerator generator)
        {
            StringBuilder lines = new StringBuilder();

            foreach(object apt_pk in appointment.WhereEquals("Month", month))
            {
                foreach(object procedure_pk in procedures.WhereEquals("AppointmentID", apt_pk))
                {
                    lines.AppendLine(generator((int)apt_pk, (int)procedure_pk));
                }
            }

            File.WriteAllText(path, lines.ToString());
        }

        public static void WriteMonthlySummary(string path)
        {

        }

        public static string[] GetResponseFileData(string path)
        {
            return File.Exists(path) ? File.ReadAllLines(path) : null;
        }
        
    }
}
