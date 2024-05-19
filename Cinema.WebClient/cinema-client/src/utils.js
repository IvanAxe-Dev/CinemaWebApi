import dayjs from "dayjs";

export function formatDate(dateString) {
    const dt = dayjs(dateString);
    return dt.format("DD.MM.YYYY");
}

export function getDayMonth(dateString) {
    const date = new Date(dateString);
    const day = date.getDay();
    const month = date.toLocaleString('en', { month: 'long'});
    return `${day} ${month}`;
}

export function getAverageRating(ratingArray) {
    if (!ratingArray.length > 0) return 0;
    const sum = ratingArray.reduce((total, current) => total + current, 0);
    return Math.round((sum / ratingArray.length));
}

export function formatPoster(imageSrc, width=810, height=1200) {
    const imageUrlRegex = /(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|gif|png)/g;
    const isValidImageUrl = imageUrlRegex.test(imageSrc);
    const posterPlaceholder = `https://via.placeholder.com/${width}x${height}`;

    if (isValidImageUrl) return imageSrc;

    if (imageSrc.length < 255) return posterPlaceholder;

    return `data:image/jpeg;base64,${imageSrc}`;
}

export function validateTrailerSrc(videoSrc) {
    const youtubeVideoRegex = /^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/(?:[\w\-]+\?v=|embed\/|v\/)?)([\w\-]+)(\S+)?$/;
    return youtubeVideoRegex.test(videoSrc);
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

export function validateLoginData(userData) {
    const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/gm;
    const usernameValid = userData.emailOrUsername.length < 50 && userData.emailOrUsername.length > 5;
    const passwordValid = passwordRegex.test(userData.password);
    
    let status = {invalid: new Boolean(false)};
    if (!passwordValid) status.password = new Boolean(true), status.invalid = new Boolean(true);
    if (!usernameValid) status.username = new Boolean(true), status.invalid = new Boolean(true);

    return status;
}