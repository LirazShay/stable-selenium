var parent = createParentDivWith2ButtonsInside();
document.body.innerHTML = '';
document.body.appendChild(parent);

function createParent() {
    var parent = document.createElement('div');
    parent.id = 'parentDiv';
    return parent;
}
function createButton(btnId) {
    var btn = document.createElement('button');
    btn.textContent = btnId;
    btn.id = btnId;
    btn.onclick = function () {
        document.body.append(btnId + ' clicked');
        document.body.appendChild(document.createElement('br'));
    }
    return btn;
}
function createParentDivWith2ButtonsInside() {
    var parent = createParent();
    var child1 = createButton('button1');
    var child2 = createButton('button2');
    parent.appendChild(child1);
    parent.appendChild(child2);
    return parent;
}