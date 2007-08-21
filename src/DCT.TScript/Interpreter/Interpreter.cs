namespace DCT.TScript.Interpreter
{
    static class Interpreter
    {
        public static bool Interpret(string line)
        {
            string op = line.Substring(0, line.IndexOf(" "));
            string args = line.Substring(line.IndexOf(" ") + 1);
            switch(op)
            {
                case "move":
                    int id;
                    if(!int.TryParse(args, out id))
                    {
                        return false;
                    }
                    // move to id
                    break;
            }

            return true;
        }
    }
}
