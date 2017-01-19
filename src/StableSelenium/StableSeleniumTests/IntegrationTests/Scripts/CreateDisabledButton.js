createDisabledButton();
function createDisabledButton() {
    document.body.innerHTML = '';
    var btn = document.createElement('button');
    btn.textContent = 'Click me';
    btn.onclick = function () {
        document.body.append('button clicked');
    }
    btn.disabled = true;
    document.body.appendChild(btn);
}