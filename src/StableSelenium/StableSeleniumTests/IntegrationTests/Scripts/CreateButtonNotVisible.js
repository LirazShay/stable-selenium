createNotVisibleButton();
function createNotVisibleButton() {
    document.body.innerHTML = '';
    var btn = document.createElement('button');
    btn.textContent = 'Click me';
    btn.onclick = function () {
        document.body.append('button clicked');
    }
    btn.style.display = 'none';
    document.body.appendChild(btn);
}