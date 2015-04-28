// Dictionary of all loggers.
var loggers = {
    coreLogger: log4javascript.getLogger("app.core.logger"),
    modelViewLogger: log4javascript.getLogger("app.modelviews.logger")
};

// Setup logging on document ready event.
jQuery(function($) {
    var appender = new log4javascript.BrowserConsoleAppender();
    var appenderLayout = new log4javascript.PatternLayout("%d{HH:mm:ss} %-5p - %m%n");
    appender.setLayout(appenderLayout);

    for (var logger in loggers) {
        if (!loggers.hasOwnProperty(logger)) return;
        loggers[logger].addAppender(appender);
        loggers[logger].setLevel(log4javascript.Level.DEBUG);
    }
});

// Enumeration of all supported user preferences.
var userPreferences = {
    // First connect detect
    "hasLoggedOn": function() {
        return "app.auth.hasLoggedOn";
    },
    // Language preference
    "locale": function() {
        return "app.locale";
    },
    // Default club selected after login
    "defaultClub": function() {
        return "app.context.defaultClub";
    }
};

// Enumeration of all supported rest operations.
var operations = {
    "get": function() {
        return "GET";
    },
    "create": function() {
        return "POST";
    },
    "update": function() {
        return "PUT";
    },
    "delete": function() {
        return "DELETE";
    }
};

// Enumeration of all model view modes.
var viewmodes = {
    "view": function() {
        return "v";
    },
    "edition": function() {
        return "e";
    },
    "creation": function() {
        return "c";
    },
    "deletion": function() {
        return "d";
    }
};

// Enumeration of all of the application's modules.
var modules = {
    commanditaires: function() {
        return "commanditaires";
    },
    membres: function() {
        return "membres";
    }
};

// Enumeration of all of the application's claims.
var claims = {
    read: function() {
        return "Read";
    },
    readAll: function() {
        return "Read, ReadAll";
    },
    create: function() {
        return "Create";
    },
    createAll: function() {
        return "Create, CreateAll";
    },
    update: function() {
        return "Update";
    },
    updateAll: function() {
        return "Update, UpdateAll";
    },
    "delete": function() {
        return "Delete";
    },
    deleteAll: function() {
        return "Delete, DeleteAll";
    },
    admin: function() {
        return "Admin";
    },
    all: function() {
        return [readAll(), createAll(), updateAll(), deleteAll(), admin()].join(", ");
    }
};

// Utility function for rest api calls.
function restCall(uri, operation, auth, data) {
    var request = {
        url: uri,
        type: operation,
        cache: operation === operations.get(),
        dataType: "json",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(data),
        beforeSend: function(xhr) {
            // Set the authorization header if the information exists.
            if (auth) {
                xhr.setRequestHeader("Authorization", auth);
            }

            loggers.coreLogger.debug("Sending", operation, "request at", uri);
        },
        // Invokes the rest call request. To be called whenever callbacks have been set
        // and no race condition can happen.
        invoke: function() {
            // Invoke the request using jquery ajax.
            $.ajax(this);
        },
        // Adds a callback on request success.
        done: function(func) {
            // Get the old callback.
            var callback = this.success;

            // Recreate the real ajax callback on success with the new callback. 
            this.success = function(payload, textStatus, jqxhr) {
                if (callback) callback(payload, textStatus, jqxhr);
                func(payload, textStatus, jqxhr);
            };

            // Chain the request object.
            return this;
        },
        // Adds a callback on request failure.
        fail: function(func) {
            // Get the old callback.
            var callback = this.error;

            // Recreate the real ajax callback on error with the new callback. 
            this.error = function(jqhxr, textStatus, exception) {
                if (callback) callback(jqhxr, textStatus, exception);
                func(jqhxr, textStatus, exception);
            };

            // Chain the request object.
            return this;
        }
    };

    return request;
};

// Utility function to build the authorization header for kerberos authentication.
function buildKerberosAuthHeader(username, password) {
    return "Kerberos " + btoa(username + ":" + password);
};

// Utility function to build the authorization header for token authentication.
// If the token is in the cookie, this method can be called without parameter.
function buildTokenAuthHeader(token) {
    token = token ? token : authenticationToken.token;
    return "Token " + token;
};

// Utility function to throw a rest exception.
function throwRestException(jqhxr, textStatus, exception) {
    var errorObject = createErrorObject(jqhxr, textStatus, exception);
    throw errorObject;
};

// Utility function to create an error object from ajax' error parameters.
function createErrorObject(jqhxr, textStatus, exception) {
    var response = JSON.parse(jqhxr.responseText);
    return {
        httpStatus: jqhxr.status,
        httpStatusText: textStatus,
        response: response
    };
};

// Utility function to build a rest resource url. The method takes a variable number of arguments.
function buildUrl() {
    var url = "";
    var urlParts = arguments;

    // For each url part given in argument.
    for (var iUrlPart = 0; iUrlPart < urlParts.length; iUrlPart++) {
        var urlPart = urlParts[iUrlPart];

        // If url part starts with a slash, remove the slash.
        if (urlPart[0] === "/") {
            urlPart.substring(1);
        }

        // If url part does not end with a slash, add a slash.
        if (urlPart[urlPart.length - 1] !== "/" && iUrlPart < arguments.length - 1) {
            urlPart += "/";
        }

        // Concatenate sanitized url part to url.
        url += urlPart;
    }

    return url;
};

// Utility function to build the skip and take query string from nullable skip and take arguments.
function buildSkipTakeQueryString(skip, take) {
    return skip && take ? sprintf("skip=%d&take=%d", skip, take) : "";
};

// Utility function that returns a data uri for a base64 encoded image.
function buildImageDataUri(base64Image) {
    return sprintf("data:%s;base64,%s", getImageMimeType(base64Image), base64Image);
};

// Utility function to detect the image type of a base64 encoded image.
function getImageMimeType(base64Image) {
    // 1. Transform first 4 bytes of base64 image.
    var bytes = window.atob(base64Image);
    var array = new Uint8Array(new ArrayBuffer(4));
    for (var i = 0; i < 4 && i < bytes.length; i++) {
        array[i] = bytes.charCodeAt(i);
    }

    // 2. First few bytes (maximum 4) of data stream is the image type signature. Naive check.
    var jpeg = new Uint8Array([255, 216, 255, 224]);
    var jpeg2 = new Uint8Array([255, 216, 255, 225]);
    if (sequenceMatches(jpeg, array) || sequenceMatches(jpeg2, array))
        return "image/jpeg";

    var png = new Uint8Array([137, 80, 78, 71]);
    if (sequenceMatches(png, array))
        return "image/png";

    var bmp = new Uint8Array([66, 77]);
    if (sequenceMatches(bmp, array))
        return "image/bmp";

    var gif = new Uint8Array([71, 73, 70]);
    if (sequenceMatches(gif, array))
        return "image/gif";

    var tiff = new Uint8Array([73, 73, 42]);
    var tiff2 = new Uint8Array([77, 77, 42]);
    if (sequenceMatches(tiff, array) || sequenceMatches(tiff2, array))
        return "image/tiff";

    return null;
};

// Utility function to test if a sequence array matches a given array.
function sequenceMatches(sequence, array) {
    for (var i = 0; i < sequence.length; i++)
        if (sequence[i] !== array[i]) return false;
    return true;
};

// Utility function to transform an array of objects to a data store.
function asStore(data) {
    var store = {};
    for (var iDatum = 0; iDatum < data.length; iDatum++) {
        var datum = data[iDatum];
        var datumId = datum.id;

        // Stored objects require an unique identifier named "id".
        if (!datumId) {
            continue;
        }

        // Store the object.
        store[datumId] = datum;
    }

    return store;
};

// Flag the jquery element as waiting for an async request.
jQuery.fn.waiting = function() {
    var zIndex = parseInt(this.css("z-index"));
    var $loadingIcon = $("<i></i>")
        .addClass("metro-icon metro-refresh-trans metro-icon-animate");
    var $loading = $("<div></div>")
        .addClass("loading")
        .css("z-index", zIndex ? zIndex + 1 : 1000)
        .append($loadingIcon);

    var position = this.css("position");
    if (position === "absolute") {
        // Handle css properties to hide an absolute dom element under a loading modal.
        $loading.css("top", this.css("top"))
            .css("left", this.css("left"));
    } else {
        // Handle css properties to hide any other dom element under a loading modal.
        if (position !== "relative") {
            // Parent needs to be relative.
            this.css("position", "relative");
        }

        $loading.css("top", 0)
            .css("left", 0)
            .height(this.height())
            .width(this.width());
    }

    // Prepend the loading screen.
    this.prepend($loading);

    // Adjust the icon position, now that the loading screen is rendered.
    $loadingIcon
        .css("top", (this.outerHeight() - $loadingIcon.height()) / 2)
        .css("left", (this.outerWidth() - $loadingIcon.width()) / 2);
    return this;
};

// Flag the jquery element as done with the async request and active again.
jQuery.fn.active = function() {
    this.find(".loading").remove();
    return this;
};

// Returns whether the jQuery element has any children matching the selector.
jQuery.fn.exists = function(selector) {
    return this.find(selector).length > 0;
};

// Returns whether the jQuery element has any element.
jQuery.fn.exists = function() {
    return this.length > 0;
};

// Get or set the id of the element.
jQuery.fn.id = function(id) {
    if (id == undefined) {
        return this.attr("id");
    } else {
        return this.attr("id", id);
    }
};

// Capture ENTER key in element and trigger a function (<div id="xxx" data-bind="enterkey: functionX">)
ko.bindingHandlers.enterkey = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();

        $(element).on("keypress", "input, textarea, select", function(e) {
            var keyCode = e.which || e.keyCode;
            if (keyCode !== 13) {
                return true;
            }

            var target = e.target;
            target.blur();

            allBindings.enterkey.call(viewModel, viewModel, target, element);

            return false;
        });
    }
};

ko.bindingHandlers.href = {
    update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
        var path = valueAccessor();
        var replaced = path.replace(/:([A-Za-z_]+)/g, function(_, token) {
            return ko.unwrap(viewModel[token]);
        });
        element.href = replaced;
    }
};

(function($) {
    $.fn.ellipsis = function(options) {

        // default option
        var defaults = {
            'row': 1, // show rows
            'onlyFullWords': false, // set to true to avoid cutting the text in the middle of a word
            'char': "...", // ellipsis
            'callback': function() {},
            'position': "tail" // middle, tail
        };

        options = $.extend(defaults, options);

        this.each(function() {
            // get element text
            var $this = $(this);
            var text = $this.text();
            var origText = text;
            var origLength = origText.length;
            var origHeight = $this.height();

            // get height
            $this.text("a");
            var lineHeight = parseFloat($this.css("lineHeight"), 10);
            var rowHeight = $this.height();
            var gapHeight = lineHeight > rowHeight ? (lineHeight - rowHeight) : 0;
            var targetHeight = gapHeight * (options.row - 1) + rowHeight * options.row;

            if (origHeight <= targetHeight) {
                $this.text(text);
                options.callback.call(this);
                return;
            }

            var start = 1, length = 0;
            var end = text.length;

            if (options.position === "tail") {
                while (start < end) { // Binary search for max length
                    length = Math.ceil((start + end) / 2);

                    $this.text(text.slice(0, length) + options["char"]);

                    if ($this.height() <= targetHeight) {
                        start = length;
                    } else {
                        end = length - 1;
                    }
                }

                text = text.slice(0, start);

                if (options.onlyFullWords) {
                    text = text.replace(/[\u00AD\w\uac00-\ud7af]+$/, ""); // remove fragment of the last word together with possible soft-hyphen characters
                }
                text += options["char"];

            } else if (options.position === "middle") {

                var sliceLength = 0;
                while (start < end) { // Binary search for max length
                    length = Math.ceil((start + end) / 2);
                    sliceLength = Math.max(origLength - length, 0);

                    $this.text(
                        origText.slice(0, Math.floor((origLength - sliceLength) / 2)) +
                        options["char"] +
                        origText.slice(Math.floor((origLength + sliceLength) / 2), origLength)
                    );

                    if ($this.height() <= targetHeight) {
                        start = length;
                    } else {
                        end = length - 1;
                    }
                }

                sliceLength = Math.max(origLength - start, 0);
                var head = origText.slice(0, Math.floor((origLength - sliceLength) / 2));
                var tail = origText.slice(Math.floor((origLength + sliceLength) / 2), origLength);

                if (options.onlyFullWords) {
                    // remove fragment of the last or first word together with possible soft-hyphen characters
                    head = head.replace(/[\u00AD\w\uac00-\ud7af]+$/, "");
                }

                text = head + options["char"] + tail;
            }

            $this.text(text);

            options.callback.call(this);
        });

        return this;
    };
})(jQuery);