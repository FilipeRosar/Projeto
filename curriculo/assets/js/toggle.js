

(function () {
const toggleButton = document.getElementById('theme-toggle');
const root = document.documentElement;


// Detecta preferência salva ou tema padrão do sistema
const savedTheme = localStorage.getItem('theme');
const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;


if (savedTheme) {
root.setAttribute('data-theme', savedTheme);
toggleButton.setAttribute('aria-pressed', savedTheme === 'dark');
} else {
const defaultTheme = systemPrefersDark ? 'dark' : 'light';
root.setAttribute('data-theme', defaultTheme);
toggleButton.setAttribute('aria-pressed', systemPrefersDark);
}


// Alterna tema ao clicar
toggleButton.addEventListener('click', () => {
const current = root.getAttribute('data-theme');
const next = current === 'light' ? 'dark' : 'light';


root.setAttribute('data-theme', next);
toggleButton.setAttribute('aria-pressed', next === 'dark');


localStorage.setItem('theme', next);
});
})();