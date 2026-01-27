namespace CSharpAppPlayground.Classes.AppSettings
{
    public static class GlobalState
    {
        /// <summary>
        /// This part is used to set a global config for RepoDB testing, the global config once set cannot be changed
        /// so just tracking the form. Even when the form is closed the config remains set for the app lifetime.
        /// Close the program to reset the values
        /// </summary>
        private static bool RepoDBGlobalConfigIsSet { get; set; } = false;  
        private static int RepoDBChoice { get; set; } = 0; // 0=disabled, 1=Mysql, 2=Postgres

        public static bool GetRepoDBGlobalConfigState()
        {
            return RepoDBGlobalConfigIsSet;
        }

        public static int GetRepoDBGlobalConfigChoice()
        {
            return RepoDBChoice;
        }

        public static void RepoDBGlobalConfigActivated(int choice)
        {
            RepoDBChoice = choice;
            RepoDBGlobalConfigIsSet = true;
        }
    }
}
