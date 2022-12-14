const API_CLIENT = require("../api_rest/api-rest-client");
const HELPERS = require('../helpers/helpers');

/**
 * Crea una asignación de un empleado a un proyecto,
 * con base a la información ingresada en el chatbot,
 * realiza la petición solicitada.
 * 
 * @param {string} body 
 * @returns 
 */
async function createRHours(body) {

    let result = false,
        sendInformation = [],
        prefix,
        randomID;

    try {

        sendInformation = HELPERS.getInformation(body);
        randomID = HELPERS.getRandomID();
        prefix = "PostHoursReport";

        body = {
            id: randomID.toString(),
            idAssign: sendInformation[0],
            timeWorked: sendInformation[1]
        }

        let response = await API_CLIENT.post(prefix, body);
        console.log("HOURSRRR3: " + response.status);
        if (response.status === 201) {

            result = "Datos: \n" +
                "1. Asignación: " + sendInformation[0] + "\n" +
                "2. Tiempo: " + sendInformation[1];

        }

    } catch (ex) {
        console.log("ERROR hours createRHours(): " + ex.message);
    }
    console.log("HOURSRRR: " + result);
    return result;
}



module.exports = {
    createRHours
}