const API_REST_CONFIG = require("../connection/api-rest-config.json");
const REQUEST_PROMISE = require('request-promise')

/**
 * Petición de tipo post a la API REST,
 * función general para cualquier endpoint
 * según la necesidad.
 * 
 * @param {string} prefix 
 * @returns 
 */
async function post(prefix, body) {

    let request = "",
        requestResponse = "",
        responseBody = "";

    try {

        if (!body) {

            request = {
                method: 'POST',
                url: API_REST_CONFIG.URL + prefix,
                rejectUnauthorized: false,
                requestCert: false,
                agent: false,
                strictSSL: false,
                resolveWithFullResponse: true
            };

            requestResponse = await REQUEST_PROMISE(request);
            responseBody = JSON.parse(requestResponse.body);

        } else {

            request = {
                method: 'POST',
                url: API_REST_CONFIG.URL + prefix,
                body: body,
                json: true,
                rejectUnauthorized: false,
                requestCert: false,
                agent: false,
                strictSSL: false,
                resolveWithFullResponse: true
            };

            requestResponse = await REQUEST_PROMISE(request);
            responseBody = requestResponse.body;

        }
        console.log("BODY: " + JSON.stringify(body));

        if (responseBody.success === 1) {
            return { status: 201, estado: true, mensaje: 'Exitoso', data: responseBody };
        } else {
            return { status: 400, estado: false, mensaje: 'Fallido', data: responseBody };
        }

    } catch (ex) {
        console.log("500: " + ex.message);
        return { status: 500, estado: false, mensaje: ex.message };
    }

}

module.exports = { post }