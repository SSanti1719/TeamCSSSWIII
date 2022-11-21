const API_CLIENT = require("../api_rest/api-rest-client");
const MESSAGES = require('../messages/general-messages.json');
const HELPERS = require('../helpers/helpers');

/**
 * Crea una asignación de un empleado a un proyecto,
 * con base a la información ingresada en el chatbot,
 * realiza la petición solicitada.
 * 
 * @param {string} body 
 * @returns 
 */
async function createAssign(body) {

    let result = false,
        sendInformation = [],
        prefix,
        randomID;

    try {

        sendInformation = HELPERS.getInformation(body);
        randomID = HELPERS.getRandomID();
        prefix = "PostAssign?id=" + randomID + "&NitProjectManager=" + sendInformation[0] +
            "&NitEmployee=" + sendInformation[1] + "&NitProject=" + sendInformation[2];

        let response = await API_CLIENT.post(prefix);

        if (response.status === 201) {

            result = "Datos: \n" +
                "1. ID: " + randomID + "\n" +
                "2. Gestor de proyecto: " + sendInformation[0] + "\n" +
                "3. Empleado: " + sendInformation[1] + "\n" +
                "4. Proyecto: " + sendInformation[2];

        }

    } catch (ex) {
        console.log("ERROR assign createAssign(): " + ex.message);
    }

    return result;
}



module.exports = {
    createAssign
}