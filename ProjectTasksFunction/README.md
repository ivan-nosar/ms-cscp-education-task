# ProjectTasksFunction

Azure Function based on Timer trigger.

Function runs by CRON expression: `*/30 * * * * *` (each 30 seconds).

> **Note**: Please note that there might be non-significant delays in data update periods.
> It's due to the serverless compute model used in underlying databases
