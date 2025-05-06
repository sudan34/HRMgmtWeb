// Theme management
document.addEventListener('DOMContentLoaded', function () {
    // Get current theme from localStorage
    const currentTheme = localStorage.getItem('theme') || 'light';

    // Apply theme
    setTheme(currentTheme);

    // Set active theme in dropdown
    document.querySelectorAll('.theme-item').forEach(item => {
        if (item.getAttribute('data-bs-theme') === currentTheme) {
            item.classList.add('active');
        }

        item.addEventListener('click', function () {
            const theme = this.getAttribute('data-bs-theme');
            setTheme(theme);
            localStorage.setItem('theme', theme);

            document.querySelectorAll('.theme-item').forEach(i => i.classList.remove('active'));
            this.classList.add('active');
        });
    });

    // Watch for system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
        if (localStorage.getItem('theme') === 'auto') {
            setTheme(e.matches ? 'dark' : 'light');
        }
    });
});

function setTheme(theme) {
    if (theme === 'auto') {
        theme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    document.documentElement.setAttribute('data-bs-theme', theme);

    // Update theme icon
    const icon = document.querySelector('#themeDropdown i');
    if (icon) {
        icon.className = theme === 'dark' ? 'fas fa-moon' : 'fas fa-sun';
    }
}