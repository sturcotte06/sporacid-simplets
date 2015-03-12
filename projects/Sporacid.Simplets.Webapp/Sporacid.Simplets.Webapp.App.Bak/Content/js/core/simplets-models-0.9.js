//// Current serialization mode. Supported values are "PascalCase" and "CamelCase".
//var serializationMode = "PascalCase";

//function AuthenticationModel() {
//    this.username = ko.observable();
//    this.password = ko.observable();
//}

//function AuthenticationToken(jqxhr, emittedFor) {
//    this.token = jqxhr.getResponseHeader("Authorization-Token"),
//    this.emittedFor = emittedFor,
//    this.emittedAt = moment(jqxhr.getResponseHeader("Authorization-Token-Emitted-At"), moment().ISO_8601),
//    this.expiresAt = moment(jqxhr.getResponseHeader("Authorization-Token-Expires-At"), moment().ISO_8601)
//}

//function ProfilModel(restResponse) {
//    this.prenom = ko.observable();
//    this.nom = ko.observable();
//    this.concentrationId = ko.observable();
//    this.public = ko.observable();
//    this.editInProgress = ko.observable();

//    if (restResponse) {
//        var extractedModel = extractModelFromRestResponse(restResponse);
//        loadModelIntoObservables(this, extractedModel);
//    }
//}

//// Loads a pojo model into a knockout observable model.
//function loadModelIntoObservables(model, extractedModel) {
//    // Iterate through each properties of the extracted model.
//    // Knockout observables are function to be called like getter and setters.
//    // We'll call the function as a setter, with the extracted property value.
//    for (var extractedProperty in extractedModel) {
//        var property = model[extractedProperty];
//        if (property) {
//            // The setter exists.
//            model[property](extractedModel[extractedProperty]);
//        }
//    }
//}

//// Function to extract a model from a rest response.
//function extractModelFromRestResponse(restResponse) {
//    if (!restResponse) {
//        throw "Rest response was empty. Unable to extract model.";
//    }

//    // Check which response type it is. Typically, we handle 2 cases:
//    //      1. Server response was an entity with its id.
//    //      2. Server response was only an entity.

//    var entity, model = {};
//    var entityId = restResponse[formatName("id")];
//    if (entityId) {
//        entity = restResponse[formatName("entity")];
//        model = { id: entityId };
//    } else {
//        entity = restResponse;
//    }

//    // Extract model from the entity properties.
//    for (var property in entity) {
//        // Javascript standard is camel case. We therefore want our models in camel case.
//        model[formatName(property, "CamelCase")] = entity[formatName(property)];
//    }

//    return model;
//}

//// Format a name in camel case or in pascal case.
//function formatName(name, mode) {
//    if (!name) {
//        throw "Cannot format name without a name.";
//    }

//    if (!mode) {
//        mode = serializationMode;
//    }

//    switch (mode) {
//        case "PascalCase":
//            name = name.substr(0, 1).toUpperCase() + name.substr(1);
//            break;
//        case "CamelCase":
//        default:
//            name = name.substr(0, 1).toLowerCase() + name.substr(1);
//            break;
//    }

//    return name;
//}