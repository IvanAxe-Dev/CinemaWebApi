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

export function formatPoster(imageUrl, width=810, height=1200) {
    const imageUrlRegex = /(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)/g;
    const posterPlaceholder = `https://via.placeholder.com/${width}x${height}`;
    const isValidImageUrl = imageUrlRegex.test(imageUrl);
    return isValidImageUrl ? imageUrl : posterPlaceholder;
}

// Divides sessions on the same day into arrays
function divideSessionsByDate(sessions) {
    return sessions.reduce((accumulator, session) => {
        const date = dayjs(session.date);

        if (!accumulator[date]) accumulator[date] = [];

        accumulator[date].push(session);

        return accumulator;
    }, {});
}

// Sorts sessions by date in ascending order while replacing headers with 'M D'
function formatDateHeaders(sessions) {
    sessions = Object.entries(sessions)
    .sort((a, b) => new Date(b[0]) - new Date(a[0]))
    .map(([date, sessions]) => ({ date, sessions }));
  
    for (let i = 0; i < sessions.length; i++) {
        sessions[i].date = getDayMonth(sessions[i].date);
    }

    return sessions;
}

export function formatSessionsData(sessions) {
    sessions = divideSessionsByDate(sessions);
    sessions = formatDateHeaders(sessions);
    return sessions;
}