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

// Sidebar toggle functionality
document.addEventListener('DOMContentLoaded', function () {
    const sidebarToggle = document.getElementById('sidebarToggle');
    const body = document.body;

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function (e) {
            e.preventDefault();
            body.classList.toggle('sidebar-open');

            // For mobile view
            if (window.innerWidth < 992) {
                body.classList.toggle('sidebar-collapsed');
            } else {
                // For desktop view
                document.querySelector('.app-sidebar').classList.toggle('collapsed');
                document.querySelector('.main-content').classList.toggle('expanded');
            }
        });
    }

    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function (e) {
        if (window.innerWidth < 992 &&
            !e.target.closest('.app-sidebar') &&
            !e.target.closest('#sidebarToggle') &&
            body.classList.contains('sidebar-open')) {
            body.classList.remove('sidebar-open');
            body.classList.add('sidebar-collapsed');
        }
    });

    // Make menu items with children expandable
    document.querySelectorAll('.nav-link[data-bs-toggle="collapse"]').forEach(link => {
        link.addEventListener('click', function (e) {
            if (this.getAttribute('href') === '#') {
                e.preventDefault();
            }

            const target = this.getAttribute('data-bs-target');
            const collapse = document.querySelector(target);

            // Toggle the collapsed class
            this.classList.toggle('collapsed');

            // Manually trigger the collapse
            if (this.classList.contains('collapsed')) {
                collapse.classList.remove('show');
            } else {
                collapse.classList.add('show');
            }
        });
    });
});