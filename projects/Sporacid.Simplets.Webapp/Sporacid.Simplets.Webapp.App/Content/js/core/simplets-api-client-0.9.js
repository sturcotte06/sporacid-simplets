// Api object that contains all request helpers.
// None of the method below does anything until the invoke method is called on the returned request.
var api = {
    // Base url for the rest api.
    url: "http://localhost/services/api/v1/",
    // Module for all utility operations of the api.
    utility: {
        // No-op operation to test credentials and rights.
        noop: function(auth) {
            return app.utility.rest.call(app.utility.url.build(api.url, "no-op"), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
        },
        // Get all operation to get the api description.
        apiDescription: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "describe-api"), app.enums.operations.get);
        },
        // Get all operation to get the api's entities description.
        entitiesDescription: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "describe-entities"), app.enums.operations.get);
        },
        empty: function (entityName, auth) {
            return app.utility.rest.call(app.utility.url.build(api.url, sprintf("empty-%s", entityName)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
        }
    },
    // Module for all get operations on enumeration entities.
    enumerations: {
        // Get all operation for concentrations enumeration.
        concentrations: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/concentrations"), app.enums.operations.get);
        },
        // Get all operation for contacts types enumeration.
        typesContacts: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/types-contacts"), app.enums.operations.get);
        },
        // Get all operation for statuts suivies enumeration.
        statutsSuivies: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/statuts-suivies"), app.enums.operations.get);
        },
        // Get all operation for unites enumeration.
        unites: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/unites"), app.enums.operations.get);
        },
        // Get all operation for types commanditaires enumeration.
        typesCommanditaires: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/types-commanditaires"), app.enums.operations.get);
        },
        // Get all operation for types commanditaires enumeration.
        typesCommandites: function () {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/types-commandites"), app.enums.operations.get);
        },
        // Get all operation for types antecedents enumeration.
        typesAntecedents: function() {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/types-antecedents"), app.enums.operations.get);
        },
        // Get all operation for types fournisseurs enumeration.
        typesFournisseurs: function () {
            return app.utility.rest.call(app.utility.url.build(api.url, "enumeration/types-fournisseurs"), app.enums.operations.get);
        }
    },
    // Module for all operations on the userspace of a user.
    userspace: {
        profil: {
            // Get operation for the profil of a user.
            get: function(identity, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "profil"), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Update operation for the profil of a user.
            update: function(identity, profil, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "profil"), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), profil);
            }
        },
        preferences: {
            // Get operation for all preferences of a user.
            getAll: function(identity, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "preference", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a preference of a user.
            get: function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "preference", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a preference of a user.
            create: function(identity, preference, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "preference"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), preference);
            },
            // Update operation for a preference of a user.
            update: function(identity, id, preference, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "preference", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), preference);
            },
            // Delete operation for a preference of a user.
            "delete": function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "preference", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        antecedents: {
            // Get operation for all antecedents of a user.
            getAll: function(identity, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "antecedent", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a antecedent of a user.
            get: function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "antecedent", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a antecedent of a user.
            create: function(identity, antecedent, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "antecedent"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), antecedent);
            },
            // Update operation for a antecedent of a user.
            update: function(identity, id, antecedent, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "antecedent", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), antecedent);
            },
            // Delete operation for a antecedent of a user.
            "delete": function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "antecedent", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        contactsUrgence: {
            // Get operation for all contacts urgence of a user.
            getAll: function(identity, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "contact-urgence", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a contact urgence of a user.
            get: function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "contact-urgence", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a contact urgence of a user.
            create: function(identity, contact, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "contact-urgence"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), contact);
            },
            // Update operation for a contact urgence of a user.
            update: function(identity, id, contact, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "contact-urgence", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), contact);
            },
            // Delete operation for a contact urgence of a user.
            "delete": function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "contact-urgence", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        formations: {
            // Get operation for all formations of a user.
            getAll: function(identity, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "formation", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a formation of a user.
            get: function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "formation", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a formation of a user.
            create: function(identity, formation, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "formation"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), formation);
            },
            // Update operation for a formation of a user.
            update: function(identity, id, formation, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "formation", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), formation);
            },
            // Delete operation for a formation of a user.
            "delete": function(identity, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, identity, "formation", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        }
    },
    // Module for all app.enums.operations on clubs.
    clubs: {
        // Create operation for a club.
        create: function(club, auth) {
            return app.utility.rest.call(app.utility.url.build(api.url, "administration", "club"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), club);
        },
        // Get operation for the club subscriptions of a user.
        getSubscribedClubs: function(auth) {
            return app.utility.rest.call(app.utility.url.build(api.url, "club", "subscribed-to"), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
        },
        commanditaires: {
            // Get operation for all commanditaires of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a commanditaire of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a commanditaire of a club.
            create: function(club, commanditaire, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), commanditaire);
            },
            // Update operation for a commanditaire of a club.
            update: function(club, id, commanditaire, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), commanditaire);
            },
            // Delete operation for a commanditaire of a club.
            "delete": function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        commandites: {
            // Get operation for all commandites of a commanditaire of a club.
            getAll: function(club, commanditaireId, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", commanditaireId, "commandite", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a commandite of a commanditaire of a club.
            get: function(club, commanditaireId, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", commanditaireId, "commandite", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a commandite of a commanditaire of a club.
            create: function(club, commanditaireId, commandite, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", commanditaireId, "commandite"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), commandite);
            },
            // Update operation for a commandite of a commanditaire of a club.
            update: function(club, commanditaireId, id, commandite, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", commanditaireId, "commandite", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), commandite);
            },
            // Delete operation for a commandite of a commanditaire of a club.
            "delete": function(club, commanditaireId, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "commanditaire", commanditaireId, "commandite", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        fournisseurs: {
            // Get operation for all fournisseurs of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "fournisseur", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a fournisseur of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "fournisseur", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a fournisseur of a club.
            create: function(club, fournisseur, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "fournisseur"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), fournisseur);
            },
            // Update operation for a fournisseur of a club.
            update: function(club, id, fournisseur, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "fournisseur", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), fournisseur);
            },
            // Delete operation for a fournisseur of a club.
            "delete": function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "fournisseur", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        groupes: {
            // Get operation for all groupes of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a groupe of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a groupe of a club.
            create: function(club, groupe, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), groupe);
            },
            // Update operation for a groupe of a club.
            update: function(club, id, groupe, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), groupe);
            },
            // Delete operation for a groupe of a club.
            "delete": function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for the membres of a groupe of a club.
            addMembres: function(club, id, membreIds, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", id, "membre"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), membreIds);
            },
            // Delete operation for the membres of a groupe of a club.
            deleteMembres: function(club, id, membreIds, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "groupe", id, "membre"), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header(), membreIds);
            }
        },
        membres: {
            // Get operation for all membres of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "membre", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for all membres of a groupe of a club.
            getAllFromGroupe: function(club, groupeId, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "membre", "in", groupeId, "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a membre of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "membre", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a subscription to a club.
            subscribe: function(club, identity, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "membre", "subscribe", identity), app.enums.operations.create, auth ? auth : app.utility.auth.token.header());
            },
            // Delete operation for a subscription to a club.
            unsubscribe: function(club, identity, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "membre", "unsubscribe", identity), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        meetings: {
            // Get operation for all meetings of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "meeting", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a meeting of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "meeting", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a meeting of a club.
            create: function(club, meeting, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "meeting"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), meeting);
            },
            // Update operation for a meeting of a club.
            update: function(club, id, meeting, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "meeting", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), meeting);
            },
            // Delete operation for a meeting of a club.
            "delete": function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "meeting", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        },
        inventaire: {
            // Get operation for all items of a club.
            getAll: function(club, skip, take, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "inventaire", "?" + app.utility.url.qs.buildSkipTake(skip, take)), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Get operation for a item of a club.
            get: function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "inventaire", id), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Create operation for a item of a club.
            create: function(club, item, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "inventaire"), app.enums.operations.create, auth ? auth : app.utility.auth.token.header(), item);
            },
            // Update operation for a item of a club.
            update: function(club, id, item, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "inventaire", id), app.enums.operations.update, auth ? auth : app.utility.auth.token.header(), item);
            },
            // Delete operation for a item of a club.
            "delete": function(club, id, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, club, "inventaire", id), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        }
    },
    // Module for all operations on the application security.
    security: {
        contexts: {
            // Get operation for the claims of the user on a context.
            getClaims: function(context, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, context, "claims"), app.enums.operations.get, auth ? auth : app.utility.auth.token.header());
            },
            // Update operation for the claims of a user on a context.
            bindRoleTo: function(context, role, identity, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, context, "administration", "bind", role, "to", identity), app.enums.operations.update, auth ? auth : app.utility.auth.token.header());
            },
            // Delete operation for the claims of a user on a context.
            deleteClaims: function(context, identity, auth) {
                return app.utility.rest.call(app.utility.url.build(api.url, context, "administration", "unbind-claims-from", identity), app.enums.operations.delete, auth ? auth : app.utility.auth.token.header());
            }
        }
    }
};