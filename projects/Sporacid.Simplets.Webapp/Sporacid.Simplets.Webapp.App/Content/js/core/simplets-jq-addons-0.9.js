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

jQuery.fn.ellipsis = function(options) {
    // default option
    var defaults = {
        row: 1, // show rows
        onlyFullWords: false, // set to true to avoid cutting the text in the middle of a word
        "char": "...", // ellipsis
        callback: function() {},
        position: "tail" // middle, tail
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
                // remove fragment of the last word together with possible soft-hyphen characters
                text = text.replace(/[\u00AD\w\uac00-\ud7af]+$/, "");
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