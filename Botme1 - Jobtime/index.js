require('dotenv').config()
const qrcode = require('qrcode-terminal');
const { Client, LocalAuth } = require('whatsapp-web.js');
const MESSAGES = require('./messages/general-messages.json');
const HELPERS = require('./helpers/helpers');
const ASSIGN = require('./controllers/assign');
const HOURS_REGISTER = require('./controllers/hours');

var arrayChat = null;

const client = new Client({
    authStrategy: new LocalAuth({
        clientId: "client-one"
    })
});


client.on('authenticated', (session) => {});

client.initialize();
client.on("qr", qr => {
    qrcode.generate(qr, { small: true });
})

const send_message = [process.env.COUNTRY_CODE + process.env.NUMBER_PHONE];

client.on("ready", async() => {

    try {

        console.log("Chatbot activo y escuchando!");
        arrayChat = [];
        await send_message.map(value => {
            const chatId = value + "@c.us"
            message = MESSAGES.message_1;
            client.sendMessage(chatId, message);
        })

    } catch (ex) {
        console.log(ex.message);
    }
});


client.on("message", async(message) => {

    let mensaje = "",
        assignData = "",
        hoursData = "",
        assignResponse = "",
        hoursResponse = "";

    try {

        console.log("estado inicial arrayChat: ", arrayChat);
        console.log("message._data.from: ", message._data.from);

        var posicion = HELPERS.posObjectChatId(arrayChat, message._data.from);

        if (MESSAGES.initial.includes(message.body) && (posicion === -1 || arrayChat[posicion].state === "init")) {

            console.log("1-");

            if (posicion === -1) {
                var node = HELPERS.createNode(message._data.from, message._data.notifyName);
                arrayChat[arrayChat === [] ? 0 : arrayChat.length] = node;
                console.log([arrayChat.length - 1]);
            }

            client.sendMessage(message.from, MESSAGES.message_2);
            mensaje = MESSAGES.message_3 + message._data.notifyName + MESSAGES.message_4
            client.sendMessage(message.from, mensaje);
            client.sendMessage(message.from, MESSAGES.message_5);
            client.sendMessage(message.from, MESSAGES.message_6);

        } else if (message.body.toString().trim() === "2" && arrayChat[posicion].state === "option" && arrayChat[posicion].opcion === 0) {

            arrayChat[posicion].errores = 0;
            arrayChat = HELPERS.clearObjectChatId(arrayChat, posicion);
            arrayChat[posicion].state = "option";
            arrayChat[posicion].errores = 0;
            client.sendMessage(message.from, MESSAGES.message_7);
            client.sendMessage(message.from, MESSAGES.message_2);
            mensaje = MESSAGES.message_3 + message._data.notifyName + MESSAGES.message_4
            client.sendMessage(message.from, mensaje);
            client.sendMessage(message.from, MESSAGES.message_5);
            client.sendMessage(message.from, MESSAGES.message_6);

        } else if (message.body.toString().trim() === "1" && arrayChat[posicion].state === "option") {

            arrayChat[posicion].opcion = message.body.toString().trim();
            arrayChat[posicion].state = "assign";
            client.sendMessage(message.from, MESSAGES.message_8);

        } else if (message.body.toString().trim() === "3" && arrayChat[posicion].state === "option") {

            arrayChat[posicion].opcion = message.body.toString().trim();
            arrayChat[posicion].state = "hours";
            client.sendMessage(message.from, MESSAGES.message_13);

        } else if (["1", "2", "3", "4"].includes(arrayChat[posicion].opcion.toString().trim()) && arrayChat[posicion].state === "assign") {

            assignData = message.body.toString().trim();
            assignResponse = await ASSIGN.createAssign(assignData);

            if (!assignResponse) {

                client.sendMessage(message.from, MESSAGES.message_9);

            } else {

                client.sendMessage(message.from, MESSAGES.message_10);
                client.sendMessage(message.from, assignResponse);

            }

            arrayChat[posicion].state = "option";
            arrayChat[posicion].errores = 0;
            arrayChat[posicion].opcion = 0;
            client.sendMessage(message.from, MESSAGES.message_7);
            client.sendMessage(message.from, MESSAGES.message_2);
            mensaje = MESSAGES.message_3 + message._data.notifyName + MESSAGES.message_4
            client.sendMessage(message.from, mensaje);
            client.sendMessage(message.from, MESSAGES.message_5);
            client.sendMessage(message.from, MESSAGES.message_6);


        } else if (["1", "2", "3", "4"].includes(arrayChat[posicion].opcion.toString().trim()) && arrayChat[posicion].state === "hours") {

            hoursData = message.body.toString().trim();
            hoursResponse = await HOURS_REGISTER.createRHours(hoursData);
            console.log("HOURS: " + hoursResponse);

            if (!hoursResponse) {

                client.sendMessage(message.from, MESSAGES.message_14);

            } else {

                client.sendMessage(message.from, MESSAGES.message_15);
                client.sendMessage(message.from, hoursResponse);

            }

            arrayChat[posicion].state = "option";
            arrayChat[posicion].errores = 0;
            arrayChat[posicion].opcion = 0;
            client.sendMessage(message.from, MESSAGES.message_7);
            client.sendMessage(message.from, MESSAGES.message_2);
            mensaje = MESSAGES.message_3 + message._data.notifyName + MESSAGES.message_4
            client.sendMessage(message.from, mensaje);
            client.sendMessage(message.from, MESSAGES.message_5);
            client.sendMessage(message.from, MESSAGES.message_6);


        } else {

            console.log("-");
            arrayChat[posicion].errores = arrayChat[posicion].errores + 1;

            if (arrayChat[posicion].errores > 2) {

                arrayChat[posicion].state = "init";
                arrayChat[posicion].errores = 0;
                client.sendMessage(message.from, MESSAGES.message_11);

            } else {

                client.sendMessage(message.from, MESSAGES.message_12);

            }
        }
    } catch (ex) {
        console.log(ex.message);
    }
});