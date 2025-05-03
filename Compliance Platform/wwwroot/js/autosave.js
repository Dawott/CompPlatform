let autoSaveTimerId = null;

window.startAutoSaveTimer = (dotnetHelper, intervalMs) => {
    // Zrzuć istniejące timery
    if (autoSaveTimerId) {
        clearInterval(autoSaveTimerId);
    }

    // Tutaj zapis nowego
    autoSaveTimerId = setInterval(() => {
        dotnetHelper.invokeMethodAsync('AutoSave');
    }, intervalMs);
};

window.clearAutoSaveTimer = () => {
    if (autoSaveTimerId) {
        clearInterval(autoSaveTimerId);
        autoSaveTimerId = null;
    }
};

window.showAutoSaveNotification = () => {
    // Stwórz lub update
    let notification = document.getElementById('autosave-notification');
    if (!notification) {
        notification = document.createElement('div');
        notification.id = 'autosave-notification';
        notification.style.position = 'fixed';
        notification.style.bottom = '10px';
        notification.style.right = '10px';
        notification.style.backgroundColor = '#4CAF50';
        notification.style.color = 'white';
        notification.style.padding = '10px';
        notification.style.borderRadius = '5px';
        notification.style.opacity = '0';
        notification.style.transition = 'opacity 0.5s';
        document.body.appendChild(notification);
    }

    // Notyfikacja
    notification.textContent = 'Formularz zapisany automatycznie';
    notification.style.opacity = '1';

    // Ukryj po 2 sekundach
    setTimeout(() => {
        notification.style.opacity = '0';
    }, 2000);
};