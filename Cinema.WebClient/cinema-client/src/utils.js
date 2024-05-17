import dayjs from "dayjs";

export function formatDate(dateString) {
    const dt = dayjs(dateString);
    return dt.format("DD.MM.YYYY");
}

export function getDayMonth(dateString) {
    const date = new Date(dateString);
    const day = date.getDay();
    const month = date.toLocaleString('default', { month: 'long'});
    return `${day} ${month}`;
}

export function getAverageRating(ratingArray) {
    if (!ratingArray.length > 0) return 0;
    const sum = ratingArray.reduce((total, current) => total + current, 0);
    return (sum / ratingArray.length);
}