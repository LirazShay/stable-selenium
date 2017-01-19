createNotVisibleButton();
setTimeout(makeButtonVisible, 3000);
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
function makeButtonVisible() {
    var btn = document.querySelector('button');
    btn.style.display = 'block';
}