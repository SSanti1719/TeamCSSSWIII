function clearObjectChatId(arrayChat, posicion) {

    arrayChat[posicion].state = "init";
    arrayChat[posicion].token = "";
    arrayChat[posicion].email = "";
    arrayChat[posicion].password = "";
    arrayChat[posicion].company = "";
    arrayChat[posicion].report = "";
    arrayChat[posicion].opcion = 0;
    arrayChat[posicion].errores = 0;
    arrayChat[posicion].idSolicitud = 0;
    arrayChat[posicion].params = [];
    arrayChat[posicion].posParam = -1;

    return arrayChat;

}

function posObjectChatId(arrayChat, from) {

    if (arrayChat !== []) {
        for (let i = 0; i < arrayChat.length; i++) {
            if (arrayChat[i].from === from) {
                return i;
            }
        }
    }

    return -1;
}

function createNode(from, name) {

    var node = {
        "from": from,
        "name": name,
        "state": "option",
        "token": "",
        "email": "",
        "password": "",
        "company": "",
        "report": "",
        "opcion": 0,
        "errores": 0,
        "idSolicitud": 0,
        "params": [],
        "posParam": -1
    }

    return node;
}

function getInformation(dataInformation) {

    let information = [];

    try {

        dataInformation = dataInformation.replace(/ /g, '');
        information = dataInformation.split(',');

    } catch (ex) {
        console.log("ERROR helpers getInformation(): " + ex.message);
    }

    return information;
}

function getRandomID() {

    let random;

    try {

        random = Math.floor((Math.random() * (1000 - 9999 + 1)) + 9999);

    } catch (ex) {
        console.log("ERROR helpers getRandomID(): " + ex.message);
    }

    return random;
}



module.exports = {
    clearObjectChatId,
    posObjectChatId,
    createNode,
    getInformation,
    getRandomID
}