namespace The6Bits.BitOHealth.ControllerLayer
{
    class Class1
    {
        public async void ScheduleAction(Action action, DateTime ExecutionTime)
        {
            while (true)
            {
                await Task.Delay((int)ExecutionTime.Subtract(DateTime.Now).TotalMilliseconds);
                action();
                ExecutionTime.AddMonths(1);
            }
        }

        void Archive()
        {

        }


    }
}
