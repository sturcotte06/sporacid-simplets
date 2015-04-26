// Base url for the rest api.
var apiUrl = "http://localhost/services/api/v1/";

// Api object that contains all request helpers.
// None of the method below does anything until the invoke method is called on the returned request.
var api = {
    // Module for all utility operations of the api.
    utility: {
        // No-op operation to test credentials and rights.
        noop: function(auth) {
            return restCall(buildUrl(apiUrl, "no-op"), operations.get(), auth ? auth : buildTokenAuthHeader());
        },
        // Get all operation to get the api description.
        apiDescription: function() {
            return restCall(buildUrl(apiUrl, "describe-api"), operations.get());
        },
        // Get all operation to get the api's entities description.
        entitiesDescription: function() {
            return restCall(buildUrl(apiUrl, "describe-entities"), operations.get());
        }
    },
    // Module for all get operations on enumeration entities.
    enumerations: {
        // Get all operation for concentrations enumeration.
        concentrations: function() {
            return restCall(buildUrl(apiUrl, "enumeration/concentrations"), operations.get());
        },
        // Get all operation for contacts types enumeration.
        typesContacts: function() {
            return restCall(buildUrl(apiUrl, "enumeration/types-contacts"), operations.get());
        },
        // Get all operation for statuts suivies enumeration.
        statutsSuivies: function() {
            return restCall(buildUrl(apiUrl, "enumeration/statuts-suivies"), operations.get());
        },
        // Get all operation for unites enumeration.
        unites: function() {
            return restCall(buildUrl(apiUrl, "enumeration/unites"), operations.get());
        },
        // Get all operation for unites enumeration.
        typesCommanditaires: function() {
            return restCall(buildUrl(apiUrl, "enumeration/types-commanditaires"), operations.get());
        }
    },
    // Module for all operations on the userspace of a user.
    userspace: {
        profil: {
            // Get operation for the profil of a user.
            get: function(identity, auth) {
                return restCall(buildUrl(apiUrl, identity, "profil"), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Update operation for the profil of a user.
            update: function(identity, profil, auth) {
                return restCall(buildUrl(apiUrl, identity, "profil"), operations.update(), auth ? auth : buildTokenAuthHeader(), profil);
            }
        },
        preferences: {
            // Get operation for all preferences of a user.
            getAll: function(identity, skip, take, auth) {
                return restCall(buildUrl(apiUrl, identity, "preference", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a preference of a user.
            get: function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "preference", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a preference of a user.
            create: function(identity, preference, auth) {
                return restCall(buildUrl(apiUrl, identity, "preference"), operations.create(), auth ? auth : buildTokenAuthHeader(), preference);
            },
            // Update operation for a preference of a user.
            update: function(identity, id, preference, auth) {
                return restCall(buildUrl(apiUrl, identity, "preference", id), operations.update(), auth ? auth : buildTokenAuthHeader(), preference);
            },
            // Delete operation for a preference of a user.
            "delete": function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "preference", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        antecedents: {
            // Get operation for all antecedents of a user.
            getAll: function(identity, skip, take, auth) {
                return restCall(buildUrl(apiUrl, identity, "antecedent", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a antecedent of a user.
            get: function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "antecedent", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a antecedent of a user.
            create: function(identity, antecedent, auth) {
                return restCall(buildUrl(apiUrl, identity, "antecedent"), operations.create(), auth ? auth : buildTokenAuthHeader(), antecedent);
            },
            // Update operation for a antecedent of a user.
            update: function(identity, id, antecedent, auth) {
                return restCall(buildUrl(apiUrl, identity, "antecedent", id), operations.update(), auth ? auth : buildTokenAuthHeader(), antecedent);
            },
            // Delete operation for a antecedent of a user.
            "delete": function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "antecedent", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        contactsUrgence: {
            // Get operation for all contacts urgence of a user.
            getAll: function(identity, skip, take, auth) {
                return restCall(buildUrl(apiUrl, identity, "contact-urgence", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a contact urgence of a user.
            get: function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "contact-urgence", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a contact urgence of a user.
            create: function(identity, contact, auth) {
                return restCall(buildUrl(apiUrl, identity, "contact-urgence"), operations.create(), auth ? auth : buildTokenAuthHeader(), contact);
            },
            // Update operation for a contact urgence of a user.
            update: function(identity, id, contact, auth) {
                return restCall(buildUrl(apiUrl, identity, "contact-urgence", id), operations.update(), auth ? auth : buildTokenAuthHeader(), contact);
            },
            // Delete operation for a contact urgence of a user.
            "delete": function(identity, id, auth) {
                return restCall(buildUrl(apiUrl, identity, "contact-urgence", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        }
    },
    // Module for all operations on clubs.
    clubs: {
        // Create operation for a club.
        create: function (club, auth) {
            return restCall(buildUrl(apiUrl, "administration", "club"), operations.create(), auth ? auth : buildTokenAuthHeader(), club);
        },
        // Get operation for the club subscriptions of a user.
        getSubscribedClubs: function(auth) {
            return restCall(buildUrl(apiUrl, "club", "subscribed-to"), operations.get(), auth ? auth : buildTokenAuthHeader());
        },
        commanditaires: {
            // Get operation for all commanditaires of a club.
            getAll: function(club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a commanditaire of a club.
            get: function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a commanditaire of a club.
            create: function(club, commanditaire, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire"), operations.create(), auth ? auth : buildTokenAuthHeader(), commanditaire);
            },
            // Update operation for a commanditaire of a club.
            update: function(club, id, commanditaire, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", id), operations.update(), auth ? auth : buildTokenAuthHeader(), commanditaire);
            },
            // Delete operation for a commanditaire of a club.
            "delete": function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        commandites: {
            // Get operation for all commandites of a commanditaire of a club.
            getAll: function (club, commanditaireId, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", commanditaireId, "commandite", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a commandite of a commanditaire of a club.
            get: function (club, commanditaireId, id, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", commanditaireId, "commandite", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a commandite of a commanditaire of a club.
            create: function (club, commanditaireId, commandite, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", commanditaireId, "commandite"), operations.create(), auth ? auth : buildTokenAuthHeader(), commandite);
            },
            // Update operation for a commandite of a commanditaire of a club.
            update: function (club, commanditaireId, id, commandite, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", commanditaireId, "commandite", id), operations.update(), auth ? auth : buildTokenAuthHeader(), commandite);
            },
            // Delete operation for a commandite of a commanditaire of a club.
            "delete": function (club, commanditaireId, id, auth) {
                return restCall(buildUrl(apiUrl, club, "commanditaire", commanditaireId, "commandite", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        fournisseurs: {
            // Get operation for all fournisseurs of a club.
            getAll: function(club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "fournisseur", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a fournisseur of a club.
            get: function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "fournisseur", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a fournisseur of a club.
            create: function(club, fournisseur, auth) {
                return restCall(buildUrl(apiUrl, club, "fournisseur"), operations.create(), auth ? auth : buildTokenAuthHeader(), fournisseur);
            },
            // Update operation for a fournisseur of a club.
            update: function(club, id, fournisseur, auth) {
                return restCall(buildUrl(apiUrl, club, "fournisseur", id), operations.update(), auth ? auth : buildTokenAuthHeader(), fournisseur);
            },
            // Delete operation for a fournisseur of a club.
            "delete": function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "fournisseur", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        groupes: {
            // Get operation for all groupes of a club.
            getAll: function(club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a groupe of a club.
            get: function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a groupe of a club.
            create: function(club, groupe, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe"), operations.create(), auth ? auth : buildTokenAuthHeader(), groupe);
            },
            // Update operation for a groupe of a club.
            update: function(club, id, groupe, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", id), operations.update(), auth ? auth : buildTokenAuthHeader(), groupe);
            },
            // Delete operation for a groupe of a club.
            "delete": function(club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for the membres of a groupe of a club.
            addMembres: function(club, id, membreIds, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", id, "membre"), operations.create(), auth ? auth : buildTokenAuthHeader(), membreIds);
            },
            // Delete operation for the membres of a groupe of a club.
            deleteMembres: function(club, id, membreIds, auth) {
                return restCall(buildUrl(apiUrl, club, "groupe", id, "membre"), operations.delete(), auth ? auth : buildTokenAuthHeader(), membreIds);
            }
        },
        membres: {
            // Get operation for all membres of a club.
            getAll: function(club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "membre", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for all membres of a groupe of a club.
            getAllFromGroupe: function(club, groupeId, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "membre", "in", groupeId, "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a membre of a club.
            get: function (club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "membre", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a subscription to a club.
            subscribe: function(club, identity, auth) {
                return restCall(buildUrl(apiUrl, club, "membre", "subscribe", identity), operations.create(), auth ? auth : buildTokenAuthHeader());
            },
            // Delete operation for a subscription to a club.
            unsubscribe: function (club, identity, auth) {
                return restCall(buildUrl(apiUrl, club, "membre", "unsubscribe", identity), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        meetings: {
            // Get operation for all meetings of a club.
            getAll: function (club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "meeting", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a meeting of a club.
            get: function (club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "meeting", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a meeting of a club.
            create: function (club, meeting, auth) {
                return restCall(buildUrl(apiUrl, club, "meeting"), operations.create(), auth ? auth : buildTokenAuthHeader(), meeting);
            },
            // Update operation for a meeting of a club.
            update: function (club, id, meeting, auth) {
                return restCall(buildUrl(apiUrl, club, "meeting", id), operations.update(), auth ? auth : buildTokenAuthHeader(), meeting);
            },
            // Delete operation for a meeting of a club.
            "delete": function (club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "meeting", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        },
        inventaire: {
            // Get operation for all items of a club.
            getAll: function (club, skip, take, auth) {
                return restCall(buildUrl(apiUrl, club, "inventaire", "?" + buildSkipTakeUrl(skip, take)), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Get operation for a item of a club.
            get: function (club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "inventaire", id), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Create operation for a item of a club.
            create: function (club, item, auth) {
                return restCall(buildUrl(apiUrl, club, "inventaire"), operations.create(), auth ? auth : buildTokenAuthHeader(), item);
            },
            // Update operation for a item of a club.
            update: function (club, id, item, auth) {
                return restCall(buildUrl(apiUrl, club, "inventaire", id), operations.update(), auth ? auth : buildTokenAuthHeader(), item);
            },
            // Delete operation for a item of a club.
            "delete": function (club, id, auth) {
                return restCall(buildUrl(apiUrl, club, "inventaire", id), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        }
    },
    // Module for all operations on the application security.
    security: {
        contexts: {
            // Get operation for the claims of the user on a context.
            getClaims: function(context, auth) {
                return restCall(buildUrl(apiUrl, context, "claims"), operations.get(), auth ? auth : buildTokenAuthHeader());
            },
            // Update operation for the claims of a user on a context.
            bindRoleTo: function(context, role, identity, auth) {
                return restCall(buildUrl(apiUrl, context, "administration", "bind", role, "to", identity), operations.update(), auth ? auth : buildTokenAuthHeader());
            },
            // Delete operation for the claims of a user on a context.
            deleteClaims: function(context, identity, auth) {
                return restCall(buildUrl(apiUrl, context, "administration", "unbind-claims-from", identity), operations.delete(), auth ? auth : buildTokenAuthHeader());
            }
        }
    }
};