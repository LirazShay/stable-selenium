setTimeout(createButton, 3000);
function createButton() {
    document.body.innerHTML = '';
    var btn = document.createElement('button');
    btn.textContent = 'Click me';
    btn.onclick = function () {
        document.body.append('button clicked');
    }
    document.body.appendChild(btn);
}