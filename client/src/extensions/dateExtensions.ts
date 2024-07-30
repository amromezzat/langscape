const millisecondsPerSecond = 1000;
const secondsPerMinute = 60;
const minutesPerHour = 60;
const hoursPerDay = 24;

export default function getDateFromDays(days: number) {
    const now = new Date();
    const targetDate = new Date(now.getTime() + days * hoursPerDay * minutesPerHour * secondsPerMinute * millisecondsPerSecond);
    return targetDate;
}