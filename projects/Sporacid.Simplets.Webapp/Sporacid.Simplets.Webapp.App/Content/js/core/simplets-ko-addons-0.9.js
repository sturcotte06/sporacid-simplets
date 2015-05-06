// Capture ENTER key in element and trigger a function (<div id="xxx" data-bind="enterkey: functionX">)
ko.bindingHandlers.enterkey = {
    init: function(element, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        $(element).on("keypress", "input, textarea, select", function(e) {
            var keyCode = e.which || e.keyCode;
            if (keyCode !== 13) return true;

            var target = e.target;
            target.blur();
            allBindings.enterkey.call(viewModel, viewModel, target, element);
            return false;
        });
    }
};

ko.bindingHandlers.keyvalue = {
    makeTemplateValueAccessor: function(valueAccessor) {
        return function() {
            var values = valueAccessor();
            var array = [];
            for (var key in values) {
                if (values.hasOwnProperty(key))array.push({ key: key, value: values[key] });
            }
            return array;
        };
    },
    'init': function(element, valueAccessor, allBindings, viewModel, bindingContext) {
        return ko.bindingHandlers["foreach"]["init"](element, ko.bindingHandlers["keyvalue"].makeTemplateValueAccessor(valueAccessor));
    },
    'update': function(element, valueAccessor, allBindings, viewModel, bindingContext) {
        return ko.bindingHandlers["foreach"]["update"](element, ko.bindingHandlers["keyvalue"].makeTemplateValueAccessor(valueAccessor), allBindings, viewModel, bindingContext);
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

ko.bindingHandlers.dblclick = {
    init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element).on("dblclick", function(event) {
            valueAccessor()(viewModel, event);
        });
    }
};