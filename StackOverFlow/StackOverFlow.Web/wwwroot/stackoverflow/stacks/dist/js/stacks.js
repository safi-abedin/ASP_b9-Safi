(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory();
	else if(typeof define === 'function' && define.amd)
		define([], factory);
	else if(typeof exports === 'object')
		exports["Stacks"] = factory();
	else
		root["Stacks"] = factory();
})(globalThis, () => {
return /******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	// The require scope
/******/ 	var __webpack_require__ = {};
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/define property getters */
/******/ 	(() => {
/******/ 		// define getter functions for harmony exports
/******/ 		__webpack_require__.d = (exports, definition) => {
/******/ 			for(var key in definition) {
/******/ 				if(__webpack_require__.o(definition, key) && !__webpack_require__.o(exports, key)) {
/******/ 					Object.defineProperty(exports, key, { enumerable: true, get: definition[key] });
/******/ 				}
/******/ 			}
/******/ 		};
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/hasOwnProperty shorthand */
/******/ 	(() => {
/******/ 		__webpack_require__.o = (obj, prop) => (Object.prototype.hasOwnProperty.call(obj, prop))
/******/ 	})();
/******/ 	
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	(() => {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = (exports) => {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	})();
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// ESM COMPAT FLAG
__webpack_require__.r(__webpack_exports__);

// EXPORTS
__webpack_require__.d(__webpack_exports__, {
  BannerController: () => (/* reexport */ BannerController),
  BasePopoverController: () => (/* reexport */ BasePopoverController),
  ExpandableController: () => (/* reexport */ ExpandableController),
  ModalController: () => (/* reexport */ ModalController),
  PopoverController: () => (/* reexport */ PopoverController),
  StacksApplication: () => (/* reexport */ StacksApplication),
  StacksController: () => (/* reexport */ StacksController),
  TabListController: () => (/* reexport */ TabListController),
  TableController: () => (/* reexport */ TableController),
  ToastController: () => (/* reexport */ ToastController),
  TooltipController: () => (/* reexport */ TooltipController),
  UploaderController: () => (/* reexport */ UploaderController),
  addController: () => (/* reexport */ addController),
  application: () => (/* reexport */ application),
  attachPopover: () => (/* reexport */ attachPopover),
  createController: () => (/* reexport */ createController),
  detachPopover: () => (/* reexport */ detachPopover),
  hideBanner: () => (/* reexport */ hideBanner),
  hideModal: () => (/* reexport */ hideModal),
  hidePopover: () => (/* reexport */ hidePopover),
  hideToast: () => (/* reexport */ hideToast),
  setTooltipHtml: () => (/* reexport */ setTooltipHtml),
  setTooltipText: () => (/* reexport */ setTooltipText),
  showBanner: () => (/* reexport */ showBanner),
  showModal: () => (/* reexport */ showModal),
  showPopover: () => (/* reexport */ showPopover),
  showToast: () => (/* reexport */ showToast)
});

;// CONCATENATED MODULE: ./node_modules/@hotwired/stimulus/dist/stimulus.js
/*
Stimulus 3.2.1
Copyright © 2023 Basecamp, LLC
 */
class EventListener {
    constructor(eventTarget, eventName, eventOptions) {
        this.eventTarget = eventTarget;
        this.eventName = eventName;
        this.eventOptions = eventOptions;
        this.unorderedBindings = new Set();
    }
    connect() {
        this.eventTarget.addEventListener(this.eventName, this, this.eventOptions);
    }
    disconnect() {
        this.eventTarget.removeEventListener(this.eventName, this, this.eventOptions);
    }
    bindingConnected(binding) {
        this.unorderedBindings.add(binding);
    }
    bindingDisconnected(binding) {
        this.unorderedBindings.delete(binding);
    }
    handleEvent(event) {
        const extendedEvent = extendEvent(event);
        for (const binding of this.bindings) {
            if (extendedEvent.immediatePropagationStopped) {
                break;
            }
            else {
                binding.handleEvent(extendedEvent);
            }
        }
    }
    hasBindings() {
        return this.unorderedBindings.size > 0;
    }
    get bindings() {
        return Array.from(this.unorderedBindings).sort((left, right) => {
            const leftIndex = left.index, rightIndex = right.index;
            return leftIndex < rightIndex ? -1 : leftIndex > rightIndex ? 1 : 0;
        });
    }
}
function extendEvent(event) {
    if ("immediatePropagationStopped" in event) {
        return event;
    }
    else {
        const { stopImmediatePropagation } = event;
        return Object.assign(event, {
            immediatePropagationStopped: false,
            stopImmediatePropagation() {
                this.immediatePropagationStopped = true;
                stopImmediatePropagation.call(this);
            },
        });
    }
}

class Dispatcher {
    constructor(application) {
        this.application = application;
        this.eventListenerMaps = new Map();
        this.started = false;
    }
    start() {
        if (!this.started) {
            this.started = true;
            this.eventListeners.forEach((eventListener) => eventListener.connect());
        }
    }
    stop() {
        if (this.started) {
            this.started = false;
            this.eventListeners.forEach((eventListener) => eventListener.disconnect());
        }
    }
    get eventListeners() {
        return Array.from(this.eventListenerMaps.values()).reduce((listeners, map) => listeners.concat(Array.from(map.values())), []);
    }
    bindingConnected(binding) {
        this.fetchEventListenerForBinding(binding).bindingConnected(binding);
    }
    bindingDisconnected(binding, clearEventListeners = false) {
        this.fetchEventListenerForBinding(binding).bindingDisconnected(binding);
        if (clearEventListeners)
            this.clearEventListenersForBinding(binding);
    }
    handleError(error, message, detail = {}) {
        this.application.handleError(error, `Error ${message}`, detail);
    }
    clearEventListenersForBinding(binding) {
        const eventListener = this.fetchEventListenerForBinding(binding);
        if (!eventListener.hasBindings()) {
            eventListener.disconnect();
            this.removeMappedEventListenerFor(binding);
        }
    }
    removeMappedEventListenerFor(binding) {
        const { eventTarget, eventName, eventOptions } = binding;
        const eventListenerMap = this.fetchEventListenerMapForEventTarget(eventTarget);
        const cacheKey = this.cacheKey(eventName, eventOptions);
        eventListenerMap.delete(cacheKey);
        if (eventListenerMap.size == 0)
            this.eventListenerMaps.delete(eventTarget);
    }
    fetchEventListenerForBinding(binding) {
        const { eventTarget, eventName, eventOptions } = binding;
        return this.fetchEventListener(eventTarget, eventName, eventOptions);
    }
    fetchEventListener(eventTarget, eventName, eventOptions) {
        const eventListenerMap = this.fetchEventListenerMapForEventTarget(eventTarget);
        const cacheKey = this.cacheKey(eventName, eventOptions);
        let eventListener = eventListenerMap.get(cacheKey);
        if (!eventListener) {
            eventListener = this.createEventListener(eventTarget, eventName, eventOptions);
            eventListenerMap.set(cacheKey, eventListener);
        }
        return eventListener;
    }
    createEventListener(eventTarget, eventName, eventOptions) {
        const eventListener = new EventListener(eventTarget, eventName, eventOptions);
        if (this.started) {
            eventListener.connect();
        }
        return eventListener;
    }
    fetchEventListenerMapForEventTarget(eventTarget) {
        let eventListenerMap = this.eventListenerMaps.get(eventTarget);
        if (!eventListenerMap) {
            eventListenerMap = new Map();
            this.eventListenerMaps.set(eventTarget, eventListenerMap);
        }
        return eventListenerMap;
    }
    cacheKey(eventName, eventOptions) {
        const parts = [eventName];
        Object.keys(eventOptions)
            .sort()
            .forEach((key) => {
            parts.push(`${eventOptions[key] ? "" : "!"}${key}`);
        });
        return parts.join(":");
    }
}

const defaultActionDescriptorFilters = {
    stop({ event, value }) {
        if (value)
            event.stopPropagation();
        return true;
    },
    prevent({ event, value }) {
        if (value)
            event.preventDefault();
        return true;
    },
    self({ event, value, element }) {
        if (value) {
            return element === event.target;
        }
        else {
            return true;
        }
    },
};
const descriptorPattern = /^(?:(?:([^.]+?)\+)?(.+?)(?:\.(.+?))?(?:@(window|document))?->)?(.+?)(?:#([^:]+?))(?::(.+))?$/;
function parseActionDescriptorString(descriptorString) {
    const source = descriptorString.trim();
    const matches = source.match(descriptorPattern) || [];
    let eventName = matches[2];
    let keyFilter = matches[3];
    if (keyFilter && !["keydown", "keyup", "keypress"].includes(eventName)) {
        eventName += `.${keyFilter}`;
        keyFilter = "";
    }
    return {
        eventTarget: parseEventTarget(matches[4]),
        eventName,
        eventOptions: matches[7] ? parseEventOptions(matches[7]) : {},
        identifier: matches[5],
        methodName: matches[6],
        keyFilter: matches[1] || keyFilter,
    };
}
function parseEventTarget(eventTargetName) {
    if (eventTargetName == "window") {
        return window;
    }
    else if (eventTargetName == "document") {
        return document;
    }
}
function parseEventOptions(eventOptions) {
    return eventOptions
        .split(":")
        .reduce((options, token) => Object.assign(options, { [token.replace(/^!/, "")]: !/^!/.test(token) }), {});
}
function stringifyEventTarget(eventTarget) {
    if (eventTarget == window) {
        return "window";
    }
    else if (eventTarget == document) {
        return "document";
    }
}

function camelize(value) {
    return value.replace(/(?:[_-])([a-z0-9])/g, (_, char) => char.toUpperCase());
}
function namespaceCamelize(value) {
    return camelize(value.replace(/--/g, "-").replace(/__/g, "_"));
}
function capitalize(value) {
    return value.charAt(0).toUpperCase() + value.slice(1);
}
function dasherize(value) {
    return value.replace(/([A-Z])/g, (_, char) => `-${char.toLowerCase()}`);
}
function tokenize(value) {
    return value.match(/[^\s]+/g) || [];
}

function isSomething(object) {
    return object !== null && object !== undefined;
}
function hasProperty(object, property) {
    return Object.prototype.hasOwnProperty.call(object, property);
}

const allModifiers = ["meta", "ctrl", "alt", "shift"];
class Action {
    constructor(element, index, descriptor, schema) {
        this.element = element;
        this.index = index;
        this.eventTarget = descriptor.eventTarget || element;
        this.eventName = descriptor.eventName || getDefaultEventNameForElement(element) || error("missing event name");
        this.eventOptions = descriptor.eventOptions || {};
        this.identifier = descriptor.identifier || error("missing identifier");
        this.methodName = descriptor.methodName || error("missing method name");
        this.keyFilter = descriptor.keyFilter || "";
        this.schema = schema;
    }
    static forToken(token, schema) {
        return new this(token.element, token.index, parseActionDescriptorString(token.content), schema);
    }
    toString() {
        const eventFilter = this.keyFilter ? `.${this.keyFilter}` : "";
        const eventTarget = this.eventTargetName ? `@${this.eventTargetName}` : "";
        return `${this.eventName}${eventFilter}${eventTarget}->${this.identifier}#${this.methodName}`;
    }
    shouldIgnoreKeyboardEvent(event) {
        if (!this.keyFilter) {
            return false;
        }
        const filters = this.keyFilter.split("+");
        if (this.keyFilterDissatisfied(event, filters)) {
            return true;
        }
        const standardFilter = filters.filter((key) => !allModifiers.includes(key))[0];
        if (!standardFilter) {
            return false;
        }
        if (!hasProperty(this.keyMappings, standardFilter)) {
            error(`contains unknown key filter: ${this.keyFilter}`);
        }
        return this.keyMappings[standardFilter].toLowerCase() !== event.key.toLowerCase();
    }
    shouldIgnoreMouseEvent(event) {
        if (!this.keyFilter) {
            return false;
        }
        const filters = [this.keyFilter];
        if (this.keyFilterDissatisfied(event, filters)) {
            return true;
        }
        return false;
    }
    get params() {
        const params = {};
        const pattern = new RegExp(`^data-${this.identifier}-(.+)-param$`, "i");
        for (const { name, value } of Array.from(this.element.attributes)) {
            const match = name.match(pattern);
            const key = match && match[1];
            if (key) {
                params[camelize(key)] = typecast(value);
            }
        }
        return params;
    }
    get eventTargetName() {
        return stringifyEventTarget(this.eventTarget);
    }
    get keyMappings() {
        return this.schema.keyMappings;
    }
    keyFilterDissatisfied(event, filters) {
        const [meta, ctrl, alt, shift] = allModifiers.map((modifier) => filters.includes(modifier));
        return event.metaKey !== meta || event.ctrlKey !== ctrl || event.altKey !== alt || event.shiftKey !== shift;
    }
}
const defaultEventNames = {
    a: () => "click",
    button: () => "click",
    form: () => "submit",
    details: () => "toggle",
    input: (e) => (e.getAttribute("type") == "submit" ? "click" : "input"),
    select: () => "change",
    textarea: () => "input",
};
function getDefaultEventNameForElement(element) {
    const tagName = element.tagName.toLowerCase();
    if (tagName in defaultEventNames) {
        return defaultEventNames[tagName](element);
    }
}
function error(message) {
    throw new Error(message);
}
function typecast(value) {
    try {
        return JSON.parse(value);
    }
    catch (o_O) {
        return value;
    }
}

class Binding {
    constructor(context, action) {
        this.context = context;
        this.action = action;
    }
    get index() {
        return this.action.index;
    }
    get eventTarget() {
        return this.action.eventTarget;
    }
    get eventOptions() {
        return this.action.eventOptions;
    }
    get identifier() {
        return this.context.identifier;
    }
    handleEvent(event) {
        const actionEvent = this.prepareActionEvent(event);
        if (this.willBeInvokedByEvent(event) && this.applyEventModifiers(actionEvent)) {
            this.invokeWithEvent(actionEvent);
        }
    }
    get eventName() {
        return this.action.eventName;
    }
    get method() {
        const method = this.controller[this.methodName];
        if (typeof method == "function") {
            return method;
        }
        throw new Error(`Action "${this.action}" references undefined method "${this.methodName}"`);
    }
    applyEventModifiers(event) {
        const { element } = this.action;
        const { actionDescriptorFilters } = this.context.application;
        const { controller } = this.context;
        let passes = true;
        for (const [name, value] of Object.entries(this.eventOptions)) {
            if (name in actionDescriptorFilters) {
                const filter = actionDescriptorFilters[name];
                passes = passes && filter({ name, value, event, element, controller });
            }
            else {
                continue;
            }
        }
        return passes;
    }
    prepareActionEvent(event) {
        return Object.assign(event, { params: this.action.params });
    }
    invokeWithEvent(event) {
        const { target, currentTarget } = event;
        try {
            this.method.call(this.controller, event);
            this.context.logDebugActivity(this.methodName, { event, target, currentTarget, action: this.methodName });
        }
        catch (error) {
            const { identifier, controller, element, index } = this;
            const detail = { identifier, controller, element, index, event };
            this.context.handleError(error, `invoking action "${this.action}"`, detail);
        }
    }
    willBeInvokedByEvent(event) {
        const eventTarget = event.target;
        if (event instanceof KeyboardEvent && this.action.shouldIgnoreKeyboardEvent(event)) {
            return false;
        }
        if (event instanceof MouseEvent && this.action.shouldIgnoreMouseEvent(event)) {
            return false;
        }
        if (this.element === eventTarget) {
            return true;
        }
        else if (eventTarget instanceof Element && this.element.contains(eventTarget)) {
            return this.scope.containsElement(eventTarget);
        }
        else {
            return this.scope.containsElement(this.action.element);
        }
    }
    get controller() {
        return this.context.controller;
    }
    get methodName() {
        return this.action.methodName;
    }
    get element() {
        return this.scope.element;
    }
    get scope() {
        return this.context.scope;
    }
}

class ElementObserver {
    constructor(element, delegate) {
        this.mutationObserverInit = { attributes: true, childList: true, subtree: true };
        this.element = element;
        this.started = false;
        this.delegate = delegate;
        this.elements = new Set();
        this.mutationObserver = new MutationObserver((mutations) => this.processMutations(mutations));
    }
    start() {
        if (!this.started) {
            this.started = true;
            this.mutationObserver.observe(this.element, this.mutationObserverInit);
            this.refresh();
        }
    }
    pause(callback) {
        if (this.started) {
            this.mutationObserver.disconnect();
            this.started = false;
        }
        callback();
        if (!this.started) {
            this.mutationObserver.observe(this.element, this.mutationObserverInit);
            this.started = true;
        }
    }
    stop() {
        if (this.started) {
            this.mutationObserver.takeRecords();
            this.mutationObserver.disconnect();
            this.started = false;
        }
    }
    refresh() {
        if (this.started) {
            const matches = new Set(this.matchElementsInTree());
            for (const element of Array.from(this.elements)) {
                if (!matches.has(element)) {
                    this.removeElement(element);
                }
            }
            for (const element of Array.from(matches)) {
                this.addElement(element);
            }
        }
    }
    processMutations(mutations) {
        if (this.started) {
            for (const mutation of mutations) {
                this.processMutation(mutation);
            }
        }
    }
    processMutation(mutation) {
        if (mutation.type == "attributes") {
            this.processAttributeChange(mutation.target, mutation.attributeName);
        }
        else if (mutation.type == "childList") {
            this.processRemovedNodes(mutation.removedNodes);
            this.processAddedNodes(mutation.addedNodes);
        }
    }
    processAttributeChange(element, attributeName) {
        if (this.elements.has(element)) {
            if (this.delegate.elementAttributeChanged && this.matchElement(element)) {
                this.delegate.elementAttributeChanged(element, attributeName);
            }
            else {
                this.removeElement(element);
            }
        }
        else if (this.matchElement(element)) {
            this.addElement(element);
        }
    }
    processRemovedNodes(nodes) {
        for (const node of Array.from(nodes)) {
            const element = this.elementFromNode(node);
            if (element) {
                this.processTree(element, this.removeElement);
            }
        }
    }
    processAddedNodes(nodes) {
        for (const node of Array.from(nodes)) {
            const element = this.elementFromNode(node);
            if (element && this.elementIsActive(element)) {
                this.processTree(element, this.addElement);
            }
        }
    }
    matchElement(element) {
        return this.delegate.matchElement(element);
    }
    matchElementsInTree(tree = this.element) {
        return this.delegate.matchElementsInTree(tree);
    }
    processTree(tree, processor) {
        for (const element of this.matchElementsInTree(tree)) {
            processor.call(this, element);
        }
    }
    elementFromNode(node) {
        if (node.nodeType == Node.ELEMENT_NODE) {
            return node;
        }
    }
    elementIsActive(element) {
        if (element.isConnected != this.element.isConnected) {
            return false;
        }
        else {
            return this.element.contains(element);
        }
    }
    addElement(element) {
        if (!this.elements.has(element)) {
            if (this.elementIsActive(element)) {
                this.elements.add(element);
                if (this.delegate.elementMatched) {
                    this.delegate.elementMatched(element);
                }
            }
        }
    }
    removeElement(element) {
        if (this.elements.has(element)) {
            this.elements.delete(element);
            if (this.delegate.elementUnmatched) {
                this.delegate.elementUnmatched(element);
            }
        }
    }
}

class AttributeObserver {
    constructor(element, attributeName, delegate) {
        this.attributeName = attributeName;
        this.delegate = delegate;
        this.elementObserver = new ElementObserver(element, this);
    }
    get element() {
        return this.elementObserver.element;
    }
    get selector() {
        return `[${this.attributeName}]`;
    }
    start() {
        this.elementObserver.start();
    }
    pause(callback) {
        this.elementObserver.pause(callback);
    }
    stop() {
        this.elementObserver.stop();
    }
    refresh() {
        this.elementObserver.refresh();
    }
    get started() {
        return this.elementObserver.started;
    }
    matchElement(element) {
        return element.hasAttribute(this.attributeName);
    }
    matchElementsInTree(tree) {
        const match = this.matchElement(tree) ? [tree] : [];
        const matches = Array.from(tree.querySelectorAll(this.selector));
        return match.concat(matches);
    }
    elementMatched(element) {
        if (this.delegate.elementMatchedAttribute) {
            this.delegate.elementMatchedAttribute(element, this.attributeName);
        }
    }
    elementUnmatched(element) {
        if (this.delegate.elementUnmatchedAttribute) {
            this.delegate.elementUnmatchedAttribute(element, this.attributeName);
        }
    }
    elementAttributeChanged(element, attributeName) {
        if (this.delegate.elementAttributeValueChanged && this.attributeName == attributeName) {
            this.delegate.elementAttributeValueChanged(element, attributeName);
        }
    }
}

function add(map, key, value) {
    fetch(map, key).add(value);
}
function del(map, key, value) {
    fetch(map, key).delete(value);
    prune(map, key);
}
function fetch(map, key) {
    let values = map.get(key);
    if (!values) {
        values = new Set();
        map.set(key, values);
    }
    return values;
}
function prune(map, key) {
    const values = map.get(key);
    if (values != null && values.size == 0) {
        map.delete(key);
    }
}

class Multimap {
    constructor() {
        this.valuesByKey = new Map();
    }
    get keys() {
        return Array.from(this.valuesByKey.keys());
    }
    get values() {
        const sets = Array.from(this.valuesByKey.values());
        return sets.reduce((values, set) => values.concat(Array.from(set)), []);
    }
    get size() {
        const sets = Array.from(this.valuesByKey.values());
        return sets.reduce((size, set) => size + set.size, 0);
    }
    add(key, value) {
        add(this.valuesByKey, key, value);
    }
    delete(key, value) {
        del(this.valuesByKey, key, value);
    }
    has(key, value) {
        const values = this.valuesByKey.get(key);
        return values != null && values.has(value);
    }
    hasKey(key) {
        return this.valuesByKey.has(key);
    }
    hasValue(value) {
        const sets = Array.from(this.valuesByKey.values());
        return sets.some((set) => set.has(value));
    }
    getValuesForKey(key) {
        const values = this.valuesByKey.get(key);
        return values ? Array.from(values) : [];
    }
    getKeysForValue(value) {
        return Array.from(this.valuesByKey)
            .filter(([_key, values]) => values.has(value))
            .map(([key, _values]) => key);
    }
}

class IndexedMultimap extends Multimap {
    constructor() {
        super();
        this.keysByValue = new Map();
    }
    get values() {
        return Array.from(this.keysByValue.keys());
    }
    add(key, value) {
        super.add(key, value);
        add(this.keysByValue, value, key);
    }
    delete(key, value) {
        super.delete(key, value);
        del(this.keysByValue, value, key);
    }
    hasValue(value) {
        return this.keysByValue.has(value);
    }
    getKeysForValue(value) {
        const set = this.keysByValue.get(value);
        return set ? Array.from(set) : [];
    }
}

class SelectorObserver {
    constructor(element, selector, delegate, details) {
        this._selector = selector;
        this.details = details;
        this.elementObserver = new ElementObserver(element, this);
        this.delegate = delegate;
        this.matchesByElement = new Multimap();
    }
    get started() {
        return this.elementObserver.started;
    }
    get selector() {
        return this._selector;
    }
    set selector(selector) {
        this._selector = selector;
        this.refresh();
    }
    start() {
        this.elementObserver.start();
    }
    pause(callback) {
        this.elementObserver.pause(callback);
    }
    stop() {
        this.elementObserver.stop();
    }
    refresh() {
        this.elementObserver.refresh();
    }
    get element() {
        return this.elementObserver.element;
    }
    matchElement(element) {
        const { selector } = this;
        if (selector) {
            const matches = element.matches(selector);
            if (this.delegate.selectorMatchElement) {
                return matches && this.delegate.selectorMatchElement(element, this.details);
            }
            return matches;
        }
        else {
            return false;
        }
    }
    matchElementsInTree(tree) {
        const { selector } = this;
        if (selector) {
            const match = this.matchElement(tree) ? [tree] : [];
            const matches = Array.from(tree.querySelectorAll(selector)).filter((match) => this.matchElement(match));
            return match.concat(matches);
        }
        else {
            return [];
        }
    }
    elementMatched(element) {
        const { selector } = this;
        if (selector) {
            this.selectorMatched(element, selector);
        }
    }
    elementUnmatched(element) {
        const selectors = this.matchesByElement.getKeysForValue(element);
        for (const selector of selectors) {
            this.selectorUnmatched(element, selector);
        }
    }
    elementAttributeChanged(element, _attributeName) {
        const { selector } = this;
        if (selector) {
            const matches = this.matchElement(element);
            const matchedBefore = this.matchesByElement.has(selector, element);
            if (matches && !matchedBefore) {
                this.selectorMatched(element, selector);
            }
            else if (!matches && matchedBefore) {
                this.selectorUnmatched(element, selector);
            }
        }
    }
    selectorMatched(element, selector) {
        this.delegate.selectorMatched(element, selector, this.details);
        this.matchesByElement.add(selector, element);
    }
    selectorUnmatched(element, selector) {
        this.delegate.selectorUnmatched(element, selector, this.details);
        this.matchesByElement.delete(selector, element);
    }
}

class StringMapObserver {
    constructor(element, delegate) {
        this.element = element;
        this.delegate = delegate;
        this.started = false;
        this.stringMap = new Map();
        this.mutationObserver = new MutationObserver((mutations) => this.processMutations(mutations));
    }
    start() {
        if (!this.started) {
            this.started = true;
            this.mutationObserver.observe(this.element, { attributes: true, attributeOldValue: true });
            this.refresh();
        }
    }
    stop() {
        if (this.started) {
            this.mutationObserver.takeRecords();
            this.mutationObserver.disconnect();
            this.started = false;
        }
    }
    refresh() {
        if (this.started) {
            for (const attributeName of this.knownAttributeNames) {
                this.refreshAttribute(attributeName, null);
            }
        }
    }
    processMutations(mutations) {
        if (this.started) {
            for (const mutation of mutations) {
                this.processMutation(mutation);
            }
        }
    }
    processMutation(mutation) {
        const attributeName = mutation.attributeName;
        if (attributeName) {
            this.refreshAttribute(attributeName, mutation.oldValue);
        }
    }
    refreshAttribute(attributeName, oldValue) {
        const key = this.delegate.getStringMapKeyForAttribute(attributeName);
        if (key != null) {
            if (!this.stringMap.has(attributeName)) {
                this.stringMapKeyAdded(key, attributeName);
            }
            const value = this.element.getAttribute(attributeName);
            if (this.stringMap.get(attributeName) != value) {
                this.stringMapValueChanged(value, key, oldValue);
            }
            if (value == null) {
                const oldValue = this.stringMap.get(attributeName);
                this.stringMap.delete(attributeName);
                if (oldValue)
                    this.stringMapKeyRemoved(key, attributeName, oldValue);
            }
            else {
                this.stringMap.set(attributeName, value);
            }
        }
    }
    stringMapKeyAdded(key, attributeName) {
        if (this.delegate.stringMapKeyAdded) {
            this.delegate.stringMapKeyAdded(key, attributeName);
        }
    }
    stringMapValueChanged(value, key, oldValue) {
        if (this.delegate.stringMapValueChanged) {
            this.delegate.stringMapValueChanged(value, key, oldValue);
        }
    }
    stringMapKeyRemoved(key, attributeName, oldValue) {
        if (this.delegate.stringMapKeyRemoved) {
            this.delegate.stringMapKeyRemoved(key, attributeName, oldValue);
        }
    }
    get knownAttributeNames() {
        return Array.from(new Set(this.currentAttributeNames.concat(this.recordedAttributeNames)));
    }
    get currentAttributeNames() {
        return Array.from(this.element.attributes).map((attribute) => attribute.name);
    }
    get recordedAttributeNames() {
        return Array.from(this.stringMap.keys());
    }
}

class TokenListObserver {
    constructor(element, attributeName, delegate) {
        this.attributeObserver = new AttributeObserver(element, attributeName, this);
        this.delegate = delegate;
        this.tokensByElement = new Multimap();
    }
    get started() {
        return this.attributeObserver.started;
    }
    start() {
        this.attributeObserver.start();
    }
    pause(callback) {
        this.attributeObserver.pause(callback);
    }
    stop() {
        this.attributeObserver.stop();
    }
    refresh() {
        this.attributeObserver.refresh();
    }
    get element() {
        return this.attributeObserver.element;
    }
    get attributeName() {
        return this.attributeObserver.attributeName;
    }
    elementMatchedAttribute(element) {
        this.tokensMatched(this.readTokensForElement(element));
    }
    elementAttributeValueChanged(element) {
        const [unmatchedTokens, matchedTokens] = this.refreshTokensForElement(element);
        this.tokensUnmatched(unmatchedTokens);
        this.tokensMatched(matchedTokens);
    }
    elementUnmatchedAttribute(element) {
        this.tokensUnmatched(this.tokensByElement.getValuesForKey(element));
    }
    tokensMatched(tokens) {
        tokens.forEach((token) => this.tokenMatched(token));
    }
    tokensUnmatched(tokens) {
        tokens.forEach((token) => this.tokenUnmatched(token));
    }
    tokenMatched(token) {
        this.delegate.tokenMatched(token);
        this.tokensByElement.add(token.element, token);
    }
    tokenUnmatched(token) {
        this.delegate.tokenUnmatched(token);
        this.tokensByElement.delete(token.element, token);
    }
    refreshTokensForElement(element) {
        const previousTokens = this.tokensByElement.getValuesForKey(element);
        const currentTokens = this.readTokensForElement(element);
        const firstDifferingIndex = zip(previousTokens, currentTokens).findIndex(([previousToken, currentToken]) => !tokensAreEqual(previousToken, currentToken));
        if (firstDifferingIndex == -1) {
            return [[], []];
        }
        else {
            return [previousTokens.slice(firstDifferingIndex), currentTokens.slice(firstDifferingIndex)];
        }
    }
    readTokensForElement(element) {
        const attributeName = this.attributeName;
        const tokenString = element.getAttribute(attributeName) || "";
        return parseTokenString(tokenString, element, attributeName);
    }
}
function parseTokenString(tokenString, element, attributeName) {
    return tokenString
        .trim()
        .split(/\s+/)
        .filter((content) => content.length)
        .map((content, index) => ({ element, attributeName, content, index }));
}
function zip(left, right) {
    const length = Math.max(left.length, right.length);
    return Array.from({ length }, (_, index) => [left[index], right[index]]);
}
function tokensAreEqual(left, right) {
    return left && right && left.index == right.index && left.content == right.content;
}

class ValueListObserver {
    constructor(element, attributeName, delegate) {
        this.tokenListObserver = new TokenListObserver(element, attributeName, this);
        this.delegate = delegate;
        this.parseResultsByToken = new WeakMap();
        this.valuesByTokenByElement = new WeakMap();
    }
    get started() {
        return this.tokenListObserver.started;
    }
    start() {
        this.tokenListObserver.start();
    }
    stop() {
        this.tokenListObserver.stop();
    }
    refresh() {
        this.tokenListObserver.refresh();
    }
    get element() {
        return this.tokenListObserver.element;
    }
    get attributeName() {
        return this.tokenListObserver.attributeName;
    }
    tokenMatched(token) {
        const { element } = token;
        const { value } = this.fetchParseResultForToken(token);
        if (value) {
            this.fetchValuesByTokenForElement(element).set(token, value);
            this.delegate.elementMatchedValue(element, value);
        }
    }
    tokenUnmatched(token) {
        const { element } = token;
        const { value } = this.fetchParseResultForToken(token);
        if (value) {
            this.fetchValuesByTokenForElement(element).delete(token);
            this.delegate.elementUnmatchedValue(element, value);
        }
    }
    fetchParseResultForToken(token) {
        let parseResult = this.parseResultsByToken.get(token);
        if (!parseResult) {
            parseResult = this.parseToken(token);
            this.parseResultsByToken.set(token, parseResult);
        }
        return parseResult;
    }
    fetchValuesByTokenForElement(element) {
        let valuesByToken = this.valuesByTokenByElement.get(element);
        if (!valuesByToken) {
            valuesByToken = new Map();
            this.valuesByTokenByElement.set(element, valuesByToken);
        }
        return valuesByToken;
    }
    parseToken(token) {
        try {
            const value = this.delegate.parseValueForToken(token);
            return { value };
        }
        catch (error) {
            return { error };
        }
    }
}

class BindingObserver {
    constructor(context, delegate) {
        this.context = context;
        this.delegate = delegate;
        this.bindingsByAction = new Map();
    }
    start() {
        if (!this.valueListObserver) {
            this.valueListObserver = new ValueListObserver(this.element, this.actionAttribute, this);
            this.valueListObserver.start();
        }
    }
    stop() {
        if (this.valueListObserver) {
            this.valueListObserver.stop();
            delete this.valueListObserver;
            this.disconnectAllActions();
        }
    }
    get element() {
        return this.context.element;
    }
    get identifier() {
        return this.context.identifier;
    }
    get actionAttribute() {
        return this.schema.actionAttribute;
    }
    get schema() {
        return this.context.schema;
    }
    get bindings() {
        return Array.from(this.bindingsByAction.values());
    }
    connectAction(action) {
        const binding = new Binding(this.context, action);
        this.bindingsByAction.set(action, binding);
        this.delegate.bindingConnected(binding);
    }
    disconnectAction(action) {
        const binding = this.bindingsByAction.get(action);
        if (binding) {
            this.bindingsByAction.delete(action);
            this.delegate.bindingDisconnected(binding);
        }
    }
    disconnectAllActions() {
        this.bindings.forEach((binding) => this.delegate.bindingDisconnected(binding, true));
        this.bindingsByAction.clear();
    }
    parseValueForToken(token) {
        const action = Action.forToken(token, this.schema);
        if (action.identifier == this.identifier) {
            return action;
        }
    }
    elementMatchedValue(element, action) {
        this.connectAction(action);
    }
    elementUnmatchedValue(element, action) {
        this.disconnectAction(action);
    }
}

class ValueObserver {
    constructor(context, receiver) {
        this.context = context;
        this.receiver = receiver;
        this.stringMapObserver = new StringMapObserver(this.element, this);
        this.valueDescriptorMap = this.controller.valueDescriptorMap;
    }
    start() {
        this.stringMapObserver.start();
        this.invokeChangedCallbacksForDefaultValues();
    }
    stop() {
        this.stringMapObserver.stop();
    }
    get element() {
        return this.context.element;
    }
    get controller() {
        return this.context.controller;
    }
    getStringMapKeyForAttribute(attributeName) {
        if (attributeName in this.valueDescriptorMap) {
            return this.valueDescriptorMap[attributeName].name;
        }
    }
    stringMapKeyAdded(key, attributeName) {
        const descriptor = this.valueDescriptorMap[attributeName];
        if (!this.hasValue(key)) {
            this.invokeChangedCallback(key, descriptor.writer(this.receiver[key]), descriptor.writer(descriptor.defaultValue));
        }
    }
    stringMapValueChanged(value, name, oldValue) {
        const descriptor = this.valueDescriptorNameMap[name];
        if (value === null)
            return;
        if (oldValue === null) {
            oldValue = descriptor.writer(descriptor.defaultValue);
        }
        this.invokeChangedCallback(name, value, oldValue);
    }
    stringMapKeyRemoved(key, attributeName, oldValue) {
        const descriptor = this.valueDescriptorNameMap[key];
        if (this.hasValue(key)) {
            this.invokeChangedCallback(key, descriptor.writer(this.receiver[key]), oldValue);
        }
        else {
            this.invokeChangedCallback(key, descriptor.writer(descriptor.defaultValue), oldValue);
        }
    }
    invokeChangedCallbacksForDefaultValues() {
        for (const { key, name, defaultValue, writer } of this.valueDescriptors) {
            if (defaultValue != undefined && !this.controller.data.has(key)) {
                this.invokeChangedCallback(name, writer(defaultValue), undefined);
            }
        }
    }
    invokeChangedCallback(name, rawValue, rawOldValue) {
        const changedMethodName = `${name}Changed`;
        const changedMethod = this.receiver[changedMethodName];
        if (typeof changedMethod == "function") {
            const descriptor = this.valueDescriptorNameMap[name];
            try {
                const value = descriptor.reader(rawValue);
                let oldValue = rawOldValue;
                if (rawOldValue) {
                    oldValue = descriptor.reader(rawOldValue);
                }
                changedMethod.call(this.receiver, value, oldValue);
            }
            catch (error) {
                if (error instanceof TypeError) {
                    error.message = `Stimulus Value "${this.context.identifier}.${descriptor.name}" - ${error.message}`;
                }
                throw error;
            }
        }
    }
    get valueDescriptors() {
        const { valueDescriptorMap } = this;
        return Object.keys(valueDescriptorMap).map((key) => valueDescriptorMap[key]);
    }
    get valueDescriptorNameMap() {
        const descriptors = {};
        Object.keys(this.valueDescriptorMap).forEach((key) => {
            const descriptor = this.valueDescriptorMap[key];
            descriptors[descriptor.name] = descriptor;
        });
        return descriptors;
    }
    hasValue(attributeName) {
        const descriptor = this.valueDescriptorNameMap[attributeName];
        const hasMethodName = `has${capitalize(descriptor.name)}`;
        return this.receiver[hasMethodName];
    }
}

class TargetObserver {
    constructor(context, delegate) {
        this.context = context;
        this.delegate = delegate;
        this.targetsByName = new Multimap();
    }
    start() {
        if (!this.tokenListObserver) {
            this.tokenListObserver = new TokenListObserver(this.element, this.attributeName, this);
            this.tokenListObserver.start();
        }
    }
    stop() {
        if (this.tokenListObserver) {
            this.disconnectAllTargets();
            this.tokenListObserver.stop();
            delete this.tokenListObserver;
        }
    }
    tokenMatched({ element, content: name }) {
        if (this.scope.containsElement(element)) {
            this.connectTarget(element, name);
        }
    }
    tokenUnmatched({ element, content: name }) {
        this.disconnectTarget(element, name);
    }
    connectTarget(element, name) {
        var _a;
        if (!this.targetsByName.has(name, element)) {
            this.targetsByName.add(name, element);
            (_a = this.tokenListObserver) === null || _a === void 0 ? void 0 : _a.pause(() => this.delegate.targetConnected(element, name));
        }
    }
    disconnectTarget(element, name) {
        var _a;
        if (this.targetsByName.has(name, element)) {
            this.targetsByName.delete(name, element);
            (_a = this.tokenListObserver) === null || _a === void 0 ? void 0 : _a.pause(() => this.delegate.targetDisconnected(element, name));
        }
    }
    disconnectAllTargets() {
        for (const name of this.targetsByName.keys) {
            for (const element of this.targetsByName.getValuesForKey(name)) {
                this.disconnectTarget(element, name);
            }
        }
    }
    get attributeName() {
        return `data-${this.context.identifier}-target`;
    }
    get element() {
        return this.context.element;
    }
    get scope() {
        return this.context.scope;
    }
}

function readInheritableStaticArrayValues(constructor, propertyName) {
    const ancestors = getAncestorsForConstructor(constructor);
    return Array.from(ancestors.reduce((values, constructor) => {
        getOwnStaticArrayValues(constructor, propertyName).forEach((name) => values.add(name));
        return values;
    }, new Set()));
}
function readInheritableStaticObjectPairs(constructor, propertyName) {
    const ancestors = getAncestorsForConstructor(constructor);
    return ancestors.reduce((pairs, constructor) => {
        pairs.push(...getOwnStaticObjectPairs(constructor, propertyName));
        return pairs;
    }, []);
}
function getAncestorsForConstructor(constructor) {
    const ancestors = [];
    while (constructor) {
        ancestors.push(constructor);
        constructor = Object.getPrototypeOf(constructor);
    }
    return ancestors.reverse();
}
function getOwnStaticArrayValues(constructor, propertyName) {
    const definition = constructor[propertyName];
    return Array.isArray(definition) ? definition : [];
}
function getOwnStaticObjectPairs(constructor, propertyName) {
    const definition = constructor[propertyName];
    return definition ? Object.keys(definition).map((key) => [key, definition[key]]) : [];
}

class OutletObserver {
    constructor(context, delegate) {
        this.started = false;
        this.context = context;
        this.delegate = delegate;
        this.outletsByName = new Multimap();
        this.outletElementsByName = new Multimap();
        this.selectorObserverMap = new Map();
        this.attributeObserverMap = new Map();
    }
    start() {
        if (!this.started) {
            this.outletDefinitions.forEach((outletName) => {
                this.setupSelectorObserverForOutlet(outletName);
                this.setupAttributeObserverForOutlet(outletName);
            });
            this.started = true;
            this.dependentContexts.forEach((context) => context.refresh());
        }
    }
    refresh() {
        this.selectorObserverMap.forEach((observer) => observer.refresh());
        this.attributeObserverMap.forEach((observer) => observer.refresh());
    }
    stop() {
        if (this.started) {
            this.started = false;
            this.disconnectAllOutlets();
            this.stopSelectorObservers();
            this.stopAttributeObservers();
        }
    }
    stopSelectorObservers() {
        if (this.selectorObserverMap.size > 0) {
            this.selectorObserverMap.forEach((observer) => observer.stop());
            this.selectorObserverMap.clear();
        }
    }
    stopAttributeObservers() {
        if (this.attributeObserverMap.size > 0) {
            this.attributeObserverMap.forEach((observer) => observer.stop());
            this.attributeObserverMap.clear();
        }
    }
    selectorMatched(element, _selector, { outletName }) {
        const outlet = this.getOutlet(element, outletName);
        if (outlet) {
            this.connectOutlet(outlet, element, outletName);
        }
    }
    selectorUnmatched(element, _selector, { outletName }) {
        const outlet = this.getOutletFromMap(element, outletName);
        if (outlet) {
            this.disconnectOutlet(outlet, element, outletName);
        }
    }
    selectorMatchElement(element, { outletName }) {
        const selector = this.selector(outletName);
        const hasOutlet = this.hasOutlet(element, outletName);
        const hasOutletController = element.matches(`[${this.schema.controllerAttribute}~=${outletName}]`);
        if (selector) {
            return hasOutlet && hasOutletController && element.matches(selector);
        }
        else {
            return false;
        }
    }
    elementMatchedAttribute(_element, attributeName) {
        const outletName = this.getOutletNameFromOutletAttributeName(attributeName);
        if (outletName) {
            this.updateSelectorObserverForOutlet(outletName);
        }
    }
    elementAttributeValueChanged(_element, attributeName) {
        const outletName = this.getOutletNameFromOutletAttributeName(attributeName);
        if (outletName) {
            this.updateSelectorObserverForOutlet(outletName);
        }
    }
    elementUnmatchedAttribute(_element, attributeName) {
        const outletName = this.getOutletNameFromOutletAttributeName(attributeName);
        if (outletName) {
            this.updateSelectorObserverForOutlet(outletName);
        }
    }
    connectOutlet(outlet, element, outletName) {
        var _a;
        if (!this.outletElementsByName.has(outletName, element)) {
            this.outletsByName.add(outletName, outlet);
            this.outletElementsByName.add(outletName, element);
            (_a = this.selectorObserverMap.get(outletName)) === null || _a === void 0 ? void 0 : _a.pause(() => this.delegate.outletConnected(outlet, element, outletName));
        }
    }
    disconnectOutlet(outlet, element, outletName) {
        var _a;
        if (this.outletElementsByName.has(outletName, element)) {
            this.outletsByName.delete(outletName, outlet);
            this.outletElementsByName.delete(outletName, element);
            (_a = this.selectorObserverMap
                .get(outletName)) === null || _a === void 0 ? void 0 : _a.pause(() => this.delegate.outletDisconnected(outlet, element, outletName));
        }
    }
    disconnectAllOutlets() {
        for (const outletName of this.outletElementsByName.keys) {
            for (const element of this.outletElementsByName.getValuesForKey(outletName)) {
                for (const outlet of this.outletsByName.getValuesForKey(outletName)) {
                    this.disconnectOutlet(outlet, element, outletName);
                }
            }
        }
    }
    updateSelectorObserverForOutlet(outletName) {
        const observer = this.selectorObserverMap.get(outletName);
        if (observer) {
            observer.selector = this.selector(outletName);
        }
    }
    setupSelectorObserverForOutlet(outletName) {
        const selector = this.selector(outletName);
        const selectorObserver = new SelectorObserver(document.body, selector, this, { outletName });
        this.selectorObserverMap.set(outletName, selectorObserver);
        selectorObserver.start();
    }
    setupAttributeObserverForOutlet(outletName) {
        const attributeName = this.attributeNameForOutletName(outletName);
        const attributeObserver = new AttributeObserver(this.scope.element, attributeName, this);
        this.attributeObserverMap.set(outletName, attributeObserver);
        attributeObserver.start();
    }
    selector(outletName) {
        return this.scope.outlets.getSelectorForOutletName(outletName);
    }
    attributeNameForOutletName(outletName) {
        return this.scope.schema.outletAttributeForScope(this.identifier, outletName);
    }
    getOutletNameFromOutletAttributeName(attributeName) {
        return this.outletDefinitions.find((outletName) => this.attributeNameForOutletName(outletName) === attributeName);
    }
    get outletDependencies() {
        const dependencies = new Multimap();
        this.router.modules.forEach((module) => {
            const constructor = module.definition.controllerConstructor;
            const outlets = readInheritableStaticArrayValues(constructor, "outlets");
            outlets.forEach((outlet) => dependencies.add(outlet, module.identifier));
        });
        return dependencies;
    }
    get outletDefinitions() {
        return this.outletDependencies.getKeysForValue(this.identifier);
    }
    get dependentControllerIdentifiers() {
        return this.outletDependencies.getValuesForKey(this.identifier);
    }
    get dependentContexts() {
        const identifiers = this.dependentControllerIdentifiers;
        return this.router.contexts.filter((context) => identifiers.includes(context.identifier));
    }
    hasOutlet(element, outletName) {
        return !!this.getOutlet(element, outletName) || !!this.getOutletFromMap(element, outletName);
    }
    getOutlet(element, outletName) {
        return this.application.getControllerForElementAndIdentifier(element, outletName);
    }
    getOutletFromMap(element, outletName) {
        return this.outletsByName.getValuesForKey(outletName).find((outlet) => outlet.element === element);
    }
    get scope() {
        return this.context.scope;
    }
    get schema() {
        return this.context.schema;
    }
    get identifier() {
        return this.context.identifier;
    }
    get application() {
        return this.context.application;
    }
    get router() {
        return this.application.router;
    }
}

class Context {
    constructor(module, scope) {
        this.logDebugActivity = (functionName, detail = {}) => {
            const { identifier, controller, element } = this;
            detail = Object.assign({ identifier, controller, element }, detail);
            this.application.logDebugActivity(this.identifier, functionName, detail);
        };
        this.module = module;
        this.scope = scope;
        this.controller = new module.controllerConstructor(this);
        this.bindingObserver = new BindingObserver(this, this.dispatcher);
        this.valueObserver = new ValueObserver(this, this.controller);
        this.targetObserver = new TargetObserver(this, this);
        this.outletObserver = new OutletObserver(this, this);
        try {
            this.controller.initialize();
            this.logDebugActivity("initialize");
        }
        catch (error) {
            this.handleError(error, "initializing controller");
        }
    }
    connect() {
        this.bindingObserver.start();
        this.valueObserver.start();
        this.targetObserver.start();
        this.outletObserver.start();
        try {
            this.controller.connect();
            this.logDebugActivity("connect");
        }
        catch (error) {
            this.handleError(error, "connecting controller");
        }
    }
    refresh() {
        this.outletObserver.refresh();
    }
    disconnect() {
        try {
            this.controller.disconnect();
            this.logDebugActivity("disconnect");
        }
        catch (error) {
            this.handleError(error, "disconnecting controller");
        }
        this.outletObserver.stop();
        this.targetObserver.stop();
        this.valueObserver.stop();
        this.bindingObserver.stop();
    }
    get application() {
        return this.module.application;
    }
    get identifier() {
        return this.module.identifier;
    }
    get schema() {
        return this.application.schema;
    }
    get dispatcher() {
        return this.application.dispatcher;
    }
    get element() {
        return this.scope.element;
    }
    get parentElement() {
        return this.element.parentElement;
    }
    handleError(error, message, detail = {}) {
        const { identifier, controller, element } = this;
        detail = Object.assign({ identifier, controller, element }, detail);
        this.application.handleError(error, `Error ${message}`, detail);
    }
    targetConnected(element, name) {
        this.invokeControllerMethod(`${name}TargetConnected`, element);
    }
    targetDisconnected(element, name) {
        this.invokeControllerMethod(`${name}TargetDisconnected`, element);
    }
    outletConnected(outlet, element, name) {
        this.invokeControllerMethod(`${namespaceCamelize(name)}OutletConnected`, outlet, element);
    }
    outletDisconnected(outlet, element, name) {
        this.invokeControllerMethod(`${namespaceCamelize(name)}OutletDisconnected`, outlet, element);
    }
    invokeControllerMethod(methodName, ...args) {
        const controller = this.controller;
        if (typeof controller[methodName] == "function") {
            controller[methodName](...args);
        }
    }
}

function bless(constructor) {
    return shadow(constructor, getBlessedProperties(constructor));
}
function shadow(constructor, properties) {
    const shadowConstructor = extend(constructor);
    const shadowProperties = getShadowProperties(constructor.prototype, properties);
    Object.defineProperties(shadowConstructor.prototype, shadowProperties);
    return shadowConstructor;
}
function getBlessedProperties(constructor) {
    const blessings = readInheritableStaticArrayValues(constructor, "blessings");
    return blessings.reduce((blessedProperties, blessing) => {
        const properties = blessing(constructor);
        for (const key in properties) {
            const descriptor = blessedProperties[key] || {};
            blessedProperties[key] = Object.assign(descriptor, properties[key]);
        }
        return blessedProperties;
    }, {});
}
function getShadowProperties(prototype, properties) {
    return getOwnKeys(properties).reduce((shadowProperties, key) => {
        const descriptor = getShadowedDescriptor(prototype, properties, key);
        if (descriptor) {
            Object.assign(shadowProperties, { [key]: descriptor });
        }
        return shadowProperties;
    }, {});
}
function getShadowedDescriptor(prototype, properties, key) {
    const shadowingDescriptor = Object.getOwnPropertyDescriptor(prototype, key);
    const shadowedByValue = shadowingDescriptor && "value" in shadowingDescriptor;
    if (!shadowedByValue) {
        const descriptor = Object.getOwnPropertyDescriptor(properties, key).value;
        if (shadowingDescriptor) {
            descriptor.get = shadowingDescriptor.get || descriptor.get;
            descriptor.set = shadowingDescriptor.set || descriptor.set;
        }
        return descriptor;
    }
}
const getOwnKeys = (() => {
    if (typeof Object.getOwnPropertySymbols == "function") {
        return (object) => [...Object.getOwnPropertyNames(object), ...Object.getOwnPropertySymbols(object)];
    }
    else {
        return Object.getOwnPropertyNames;
    }
})();
const extend = (() => {
    function extendWithReflect(constructor) {
        function extended() {
            return Reflect.construct(constructor, arguments, new.target);
        }
        extended.prototype = Object.create(constructor.prototype, {
            constructor: { value: extended },
        });
        Reflect.setPrototypeOf(extended, constructor);
        return extended;
    }
    function testReflectExtension() {
        const a = function () {
            this.a.call(this);
        };
        const b = extendWithReflect(a);
        b.prototype.a = function () { };
        return new b();
    }
    try {
        testReflectExtension();
        return extendWithReflect;
    }
    catch (error) {
        return (constructor) => class extended extends constructor {
        };
    }
})();

function blessDefinition(definition) {
    return {
        identifier: definition.identifier,
        controllerConstructor: bless(definition.controllerConstructor),
    };
}

class Module {
    constructor(application, definition) {
        this.application = application;
        this.definition = blessDefinition(definition);
        this.contextsByScope = new WeakMap();
        this.connectedContexts = new Set();
    }
    get identifier() {
        return this.definition.identifier;
    }
    get controllerConstructor() {
        return this.definition.controllerConstructor;
    }
    get contexts() {
        return Array.from(this.connectedContexts);
    }
    connectContextForScope(scope) {
        const context = this.fetchContextForScope(scope);
        this.connectedContexts.add(context);
        context.connect();
    }
    disconnectContextForScope(scope) {
        const context = this.contextsByScope.get(scope);
        if (context) {
            this.connectedContexts.delete(context);
            context.disconnect();
        }
    }
    fetchContextForScope(scope) {
        let context = this.contextsByScope.get(scope);
        if (!context) {
            context = new Context(this, scope);
            this.contextsByScope.set(scope, context);
        }
        return context;
    }
}

class ClassMap {
    constructor(scope) {
        this.scope = scope;
    }
    has(name) {
        return this.data.has(this.getDataKey(name));
    }
    get(name) {
        return this.getAll(name)[0];
    }
    getAll(name) {
        const tokenString = this.data.get(this.getDataKey(name)) || "";
        return tokenize(tokenString);
    }
    getAttributeName(name) {
        return this.data.getAttributeNameForKey(this.getDataKey(name));
    }
    getDataKey(name) {
        return `${name}-class`;
    }
    get data() {
        return this.scope.data;
    }
}

class DataMap {
    constructor(scope) {
        this.scope = scope;
    }
    get element() {
        return this.scope.element;
    }
    get identifier() {
        return this.scope.identifier;
    }
    get(key) {
        const name = this.getAttributeNameForKey(key);
        return this.element.getAttribute(name);
    }
    set(key, value) {
        const name = this.getAttributeNameForKey(key);
        this.element.setAttribute(name, value);
        return this.get(key);
    }
    has(key) {
        const name = this.getAttributeNameForKey(key);
        return this.element.hasAttribute(name);
    }
    delete(key) {
        if (this.has(key)) {
            const name = this.getAttributeNameForKey(key);
            this.element.removeAttribute(name);
            return true;
        }
        else {
            return false;
        }
    }
    getAttributeNameForKey(key) {
        return `data-${this.identifier}-${dasherize(key)}`;
    }
}

class Guide {
    constructor(logger) {
        this.warnedKeysByObject = new WeakMap();
        this.logger = logger;
    }
    warn(object, key, message) {
        let warnedKeys = this.warnedKeysByObject.get(object);
        if (!warnedKeys) {
            warnedKeys = new Set();
            this.warnedKeysByObject.set(object, warnedKeys);
        }
        if (!warnedKeys.has(key)) {
            warnedKeys.add(key);
            this.logger.warn(message, object);
        }
    }
}

function attributeValueContainsToken(attributeName, token) {
    return `[${attributeName}~="${token}"]`;
}

class TargetSet {
    constructor(scope) {
        this.scope = scope;
    }
    get element() {
        return this.scope.element;
    }
    get identifier() {
        return this.scope.identifier;
    }
    get schema() {
        return this.scope.schema;
    }
    has(targetName) {
        return this.find(targetName) != null;
    }
    find(...targetNames) {
        return targetNames.reduce((target, targetName) => target || this.findTarget(targetName) || this.findLegacyTarget(targetName), undefined);
    }
    findAll(...targetNames) {
        return targetNames.reduce((targets, targetName) => [
            ...targets,
            ...this.findAllTargets(targetName),
            ...this.findAllLegacyTargets(targetName),
        ], []);
    }
    findTarget(targetName) {
        const selector = this.getSelectorForTargetName(targetName);
        return this.scope.findElement(selector);
    }
    findAllTargets(targetName) {
        const selector = this.getSelectorForTargetName(targetName);
        return this.scope.findAllElements(selector);
    }
    getSelectorForTargetName(targetName) {
        const attributeName = this.schema.targetAttributeForScope(this.identifier);
        return attributeValueContainsToken(attributeName, targetName);
    }
    findLegacyTarget(targetName) {
        const selector = this.getLegacySelectorForTargetName(targetName);
        return this.deprecate(this.scope.findElement(selector), targetName);
    }
    findAllLegacyTargets(targetName) {
        const selector = this.getLegacySelectorForTargetName(targetName);
        return this.scope.findAllElements(selector).map((element) => this.deprecate(element, targetName));
    }
    getLegacySelectorForTargetName(targetName) {
        const targetDescriptor = `${this.identifier}.${targetName}`;
        return attributeValueContainsToken(this.schema.targetAttribute, targetDescriptor);
    }
    deprecate(element, targetName) {
        if (element) {
            const { identifier } = this;
            const attributeName = this.schema.targetAttribute;
            const revisedAttributeName = this.schema.targetAttributeForScope(identifier);
            this.guide.warn(element, `target:${targetName}`, `Please replace ${attributeName}="${identifier}.${targetName}" with ${revisedAttributeName}="${targetName}". ` +
                `The ${attributeName} attribute is deprecated and will be removed in a future version of Stimulus.`);
        }
        return element;
    }
    get guide() {
        return this.scope.guide;
    }
}

class OutletSet {
    constructor(scope, controllerElement) {
        this.scope = scope;
        this.controllerElement = controllerElement;
    }
    get element() {
        return this.scope.element;
    }
    get identifier() {
        return this.scope.identifier;
    }
    get schema() {
        return this.scope.schema;
    }
    has(outletName) {
        return this.find(outletName) != null;
    }
    find(...outletNames) {
        return outletNames.reduce((outlet, outletName) => outlet || this.findOutlet(outletName), undefined);
    }
    findAll(...outletNames) {
        return outletNames.reduce((outlets, outletName) => [...outlets, ...this.findAllOutlets(outletName)], []);
    }
    getSelectorForOutletName(outletName) {
        const attributeName = this.schema.outletAttributeForScope(this.identifier, outletName);
        return this.controllerElement.getAttribute(attributeName);
    }
    findOutlet(outletName) {
        const selector = this.getSelectorForOutletName(outletName);
        if (selector)
            return this.findElement(selector, outletName);
    }
    findAllOutlets(outletName) {
        const selector = this.getSelectorForOutletName(outletName);
        return selector ? this.findAllElements(selector, outletName) : [];
    }
    findElement(selector, outletName) {
        const elements = this.scope.queryElements(selector);
        return elements.filter((element) => this.matchesElement(element, selector, outletName))[0];
    }
    findAllElements(selector, outletName) {
        const elements = this.scope.queryElements(selector);
        return elements.filter((element) => this.matchesElement(element, selector, outletName));
    }
    matchesElement(element, selector, outletName) {
        const controllerAttribute = element.getAttribute(this.scope.schema.controllerAttribute) || "";
        return element.matches(selector) && controllerAttribute.split(" ").includes(outletName);
    }
}

class Scope {
    constructor(schema, element, identifier, logger) {
        this.targets = new TargetSet(this);
        this.classes = new ClassMap(this);
        this.data = new DataMap(this);
        this.containsElement = (element) => {
            return element.closest(this.controllerSelector) === this.element;
        };
        this.schema = schema;
        this.element = element;
        this.identifier = identifier;
        this.guide = new Guide(logger);
        this.outlets = new OutletSet(this.documentScope, element);
    }
    findElement(selector) {
        return this.element.matches(selector) ? this.element : this.queryElements(selector).find(this.containsElement);
    }
    findAllElements(selector) {
        return [
            ...(this.element.matches(selector) ? [this.element] : []),
            ...this.queryElements(selector).filter(this.containsElement),
        ];
    }
    queryElements(selector) {
        return Array.from(this.element.querySelectorAll(selector));
    }
    get controllerSelector() {
        return attributeValueContainsToken(this.schema.controllerAttribute, this.identifier);
    }
    get isDocumentScope() {
        return this.element === document.documentElement;
    }
    get documentScope() {
        return this.isDocumentScope
            ? this
            : new Scope(this.schema, document.documentElement, this.identifier, this.guide.logger);
    }
}

class ScopeObserver {
    constructor(element, schema, delegate) {
        this.element = element;
        this.schema = schema;
        this.delegate = delegate;
        this.valueListObserver = new ValueListObserver(this.element, this.controllerAttribute, this);
        this.scopesByIdentifierByElement = new WeakMap();
        this.scopeReferenceCounts = new WeakMap();
    }
    start() {
        this.valueListObserver.start();
    }
    stop() {
        this.valueListObserver.stop();
    }
    get controllerAttribute() {
        return this.schema.controllerAttribute;
    }
    parseValueForToken(token) {
        const { element, content: identifier } = token;
        return this.parseValueForElementAndIdentifier(element, identifier);
    }
    parseValueForElementAndIdentifier(element, identifier) {
        const scopesByIdentifier = this.fetchScopesByIdentifierForElement(element);
        let scope = scopesByIdentifier.get(identifier);
        if (!scope) {
            scope = this.delegate.createScopeForElementAndIdentifier(element, identifier);
            scopesByIdentifier.set(identifier, scope);
        }
        return scope;
    }
    elementMatchedValue(element, value) {
        const referenceCount = (this.scopeReferenceCounts.get(value) || 0) + 1;
        this.scopeReferenceCounts.set(value, referenceCount);
        if (referenceCount == 1) {
            this.delegate.scopeConnected(value);
        }
    }
    elementUnmatchedValue(element, value) {
        const referenceCount = this.scopeReferenceCounts.get(value);
        if (referenceCount) {
            this.scopeReferenceCounts.set(value, referenceCount - 1);
            if (referenceCount == 1) {
                this.delegate.scopeDisconnected(value);
            }
        }
    }
    fetchScopesByIdentifierForElement(element) {
        let scopesByIdentifier = this.scopesByIdentifierByElement.get(element);
        if (!scopesByIdentifier) {
            scopesByIdentifier = new Map();
            this.scopesByIdentifierByElement.set(element, scopesByIdentifier);
        }
        return scopesByIdentifier;
    }
}

class Router {
    constructor(application) {
        this.application = application;
        this.scopeObserver = new ScopeObserver(this.element, this.schema, this);
        this.scopesByIdentifier = new Multimap();
        this.modulesByIdentifier = new Map();
    }
    get element() {
        return this.application.element;
    }
    get schema() {
        return this.application.schema;
    }
    get logger() {
        return this.application.logger;
    }
    get controllerAttribute() {
        return this.schema.controllerAttribute;
    }
    get modules() {
        return Array.from(this.modulesByIdentifier.values());
    }
    get contexts() {
        return this.modules.reduce((contexts, module) => contexts.concat(module.contexts), []);
    }
    start() {
        this.scopeObserver.start();
    }
    stop() {
        this.scopeObserver.stop();
    }
    loadDefinition(definition) {
        this.unloadIdentifier(definition.identifier);
        const module = new Module(this.application, definition);
        this.connectModule(module);
        const afterLoad = definition.controllerConstructor.afterLoad;
        if (afterLoad) {
            afterLoad.call(definition.controllerConstructor, definition.identifier, this.application);
        }
    }
    unloadIdentifier(identifier) {
        const module = this.modulesByIdentifier.get(identifier);
        if (module) {
            this.disconnectModule(module);
        }
    }
    getContextForElementAndIdentifier(element, identifier) {
        const module = this.modulesByIdentifier.get(identifier);
        if (module) {
            return module.contexts.find((context) => context.element == element);
        }
    }
    proposeToConnectScopeForElementAndIdentifier(element, identifier) {
        const scope = this.scopeObserver.parseValueForElementAndIdentifier(element, identifier);
        if (scope) {
            this.scopeObserver.elementMatchedValue(scope.element, scope);
        }
        else {
            console.error(`Couldn't find or create scope for identifier: "${identifier}" and element:`, element);
        }
    }
    handleError(error, message, detail) {
        this.application.handleError(error, message, detail);
    }
    createScopeForElementAndIdentifier(element, identifier) {
        return new Scope(this.schema, element, identifier, this.logger);
    }
    scopeConnected(scope) {
        this.scopesByIdentifier.add(scope.identifier, scope);
        const module = this.modulesByIdentifier.get(scope.identifier);
        if (module) {
            module.connectContextForScope(scope);
        }
    }
    scopeDisconnected(scope) {
        this.scopesByIdentifier.delete(scope.identifier, scope);
        const module = this.modulesByIdentifier.get(scope.identifier);
        if (module) {
            module.disconnectContextForScope(scope);
        }
    }
    connectModule(module) {
        this.modulesByIdentifier.set(module.identifier, module);
        const scopes = this.scopesByIdentifier.getValuesForKey(module.identifier);
        scopes.forEach((scope) => module.connectContextForScope(scope));
    }
    disconnectModule(module) {
        this.modulesByIdentifier.delete(module.identifier);
        const scopes = this.scopesByIdentifier.getValuesForKey(module.identifier);
        scopes.forEach((scope) => module.disconnectContextForScope(scope));
    }
}

const defaultSchema = {
    controllerAttribute: "data-controller",
    actionAttribute: "data-action",
    targetAttribute: "data-target",
    targetAttributeForScope: (identifier) => `data-${identifier}-target`,
    outletAttributeForScope: (identifier, outlet) => `data-${identifier}-${outlet}-outlet`,
    keyMappings: Object.assign(Object.assign({ enter: "Enter", tab: "Tab", esc: "Escape", space: " ", up: "ArrowUp", down: "ArrowDown", left: "ArrowLeft", right: "ArrowRight", home: "Home", end: "End", page_up: "PageUp", page_down: "PageDown" }, objectFromEntries("abcdefghijklmnopqrstuvwxyz".split("").map((c) => [c, c]))), objectFromEntries("0123456789".split("").map((n) => [n, n]))),
};
function objectFromEntries(array) {
    return array.reduce((memo, [k, v]) => (Object.assign(Object.assign({}, memo), { [k]: v })), {});
}

class Application {
    constructor(element = document.documentElement, schema = defaultSchema) {
        this.logger = console;
        this.debug = false;
        this.logDebugActivity = (identifier, functionName, detail = {}) => {
            if (this.debug) {
                this.logFormattedMessage(identifier, functionName, detail);
            }
        };
        this.element = element;
        this.schema = schema;
        this.dispatcher = new Dispatcher(this);
        this.router = new Router(this);
        this.actionDescriptorFilters = Object.assign({}, defaultActionDescriptorFilters);
    }
    static start(element, schema) {
        const application = new this(element, schema);
        application.start();
        return application;
    }
    async start() {
        await domReady();
        this.logDebugActivity("application", "starting");
        this.dispatcher.start();
        this.router.start();
        this.logDebugActivity("application", "start");
    }
    stop() {
        this.logDebugActivity("application", "stopping");
        this.dispatcher.stop();
        this.router.stop();
        this.logDebugActivity("application", "stop");
    }
    register(identifier, controllerConstructor) {
        this.load({ identifier, controllerConstructor });
    }
    registerActionOption(name, filter) {
        this.actionDescriptorFilters[name] = filter;
    }
    load(head, ...rest) {
        const definitions = Array.isArray(head) ? head : [head, ...rest];
        definitions.forEach((definition) => {
            if (definition.controllerConstructor.shouldLoad) {
                this.router.loadDefinition(definition);
            }
        });
    }
    unload(head, ...rest) {
        const identifiers = Array.isArray(head) ? head : [head, ...rest];
        identifiers.forEach((identifier) => this.router.unloadIdentifier(identifier));
    }
    get controllers() {
        return this.router.contexts.map((context) => context.controller);
    }
    getControllerForElementAndIdentifier(element, identifier) {
        const context = this.router.getContextForElementAndIdentifier(element, identifier);
        return context ? context.controller : null;
    }
    handleError(error, message, detail) {
        var _a;
        this.logger.error(`%s\n\n%o\n\n%o`, message, error, detail);
        (_a = window.onerror) === null || _a === void 0 ? void 0 : _a.call(window, message, "", 0, 0, error);
    }
    logFormattedMessage(identifier, functionName, detail = {}) {
        detail = Object.assign({ application: this }, detail);
        this.logger.groupCollapsed(`${identifier} #${functionName}`);
        this.logger.log("details:", Object.assign({}, detail));
        this.logger.groupEnd();
    }
}
function domReady() {
    return new Promise((resolve) => {
        if (document.readyState == "loading") {
            document.addEventListener("DOMContentLoaded", () => resolve());
        }
        else {
            resolve();
        }
    });
}

function ClassPropertiesBlessing(constructor) {
    const classes = readInheritableStaticArrayValues(constructor, "classes");
    return classes.reduce((properties, classDefinition) => {
        return Object.assign(properties, propertiesForClassDefinition(classDefinition));
    }, {});
}
function propertiesForClassDefinition(key) {
    return {
        [`${key}Class`]: {
            get() {
                const { classes } = this;
                if (classes.has(key)) {
                    return classes.get(key);
                }
                else {
                    const attribute = classes.getAttributeName(key);
                    throw new Error(`Missing attribute "${attribute}"`);
                }
            },
        },
        [`${key}Classes`]: {
            get() {
                return this.classes.getAll(key);
            },
        },
        [`has${capitalize(key)}Class`]: {
            get() {
                return this.classes.has(key);
            },
        },
    };
}

function OutletPropertiesBlessing(constructor) {
    const outlets = readInheritableStaticArrayValues(constructor, "outlets");
    return outlets.reduce((properties, outletDefinition) => {
        return Object.assign(properties, propertiesForOutletDefinition(outletDefinition));
    }, {});
}
function getOutletController(controller, element, identifier) {
    return controller.application.getControllerForElementAndIdentifier(element, identifier);
}
function getControllerAndEnsureConnectedScope(controller, element, outletName) {
    let outletController = getOutletController(controller, element, outletName);
    if (outletController)
        return outletController;
    controller.application.router.proposeToConnectScopeForElementAndIdentifier(element, outletName);
    outletController = getOutletController(controller, element, outletName);
    if (outletController)
        return outletController;
}
function propertiesForOutletDefinition(name) {
    const camelizedName = namespaceCamelize(name);
    return {
        [`${camelizedName}Outlet`]: {
            get() {
                const outletElement = this.outlets.find(name);
                const selector = this.outlets.getSelectorForOutletName(name);
                if (outletElement) {
                    const outletController = getControllerAndEnsureConnectedScope(this, outletElement, name);
                    if (outletController)
                        return outletController;
                    throw new Error(`The provided outlet element is missing an outlet controller "${name}" instance for host controller "${this.identifier}"`);
                }
                throw new Error(`Missing outlet element "${name}" for host controller "${this.identifier}". Stimulus couldn't find a matching outlet element using selector "${selector}".`);
            },
        },
        [`${camelizedName}Outlets`]: {
            get() {
                const outlets = this.outlets.findAll(name);
                if (outlets.length > 0) {
                    return outlets
                        .map((outletElement) => {
                        const outletController = getControllerAndEnsureConnectedScope(this, outletElement, name);
                        if (outletController)
                            return outletController;
                        console.warn(`The provided outlet element is missing an outlet controller "${name}" instance for host controller "${this.identifier}"`, outletElement);
                    })
                        .filter((controller) => controller);
                }
                return [];
            },
        },
        [`${camelizedName}OutletElement`]: {
            get() {
                const outletElement = this.outlets.find(name);
                const selector = this.outlets.getSelectorForOutletName(name);
                if (outletElement) {
                    return outletElement;
                }
                else {
                    throw new Error(`Missing outlet element "${name}" for host controller "${this.identifier}". Stimulus couldn't find a matching outlet element using selector "${selector}".`);
                }
            },
        },
        [`${camelizedName}OutletElements`]: {
            get() {
                return this.outlets.findAll(name);
            },
        },
        [`has${capitalize(camelizedName)}Outlet`]: {
            get() {
                return this.outlets.has(name);
            },
        },
    };
}

function TargetPropertiesBlessing(constructor) {
    const targets = readInheritableStaticArrayValues(constructor, "targets");
    return targets.reduce((properties, targetDefinition) => {
        return Object.assign(properties, propertiesForTargetDefinition(targetDefinition));
    }, {});
}
function propertiesForTargetDefinition(name) {
    return {
        [`${name}Target`]: {
            get() {
                const target = this.targets.find(name);
                if (target) {
                    return target;
                }
                else {
                    throw new Error(`Missing target element "${name}" for "${this.identifier}" controller`);
                }
            },
        },
        [`${name}Targets`]: {
            get() {
                return this.targets.findAll(name);
            },
        },
        [`has${capitalize(name)}Target`]: {
            get() {
                return this.targets.has(name);
            },
        },
    };
}

function ValuePropertiesBlessing(constructor) {
    const valueDefinitionPairs = readInheritableStaticObjectPairs(constructor, "values");
    const propertyDescriptorMap = {
        valueDescriptorMap: {
            get() {
                return valueDefinitionPairs.reduce((result, valueDefinitionPair) => {
                    const valueDescriptor = parseValueDefinitionPair(valueDefinitionPair, this.identifier);
                    const attributeName = this.data.getAttributeNameForKey(valueDescriptor.key);
                    return Object.assign(result, { [attributeName]: valueDescriptor });
                }, {});
            },
        },
    };
    return valueDefinitionPairs.reduce((properties, valueDefinitionPair) => {
        return Object.assign(properties, propertiesForValueDefinitionPair(valueDefinitionPair));
    }, propertyDescriptorMap);
}
function propertiesForValueDefinitionPair(valueDefinitionPair, controller) {
    const definition = parseValueDefinitionPair(valueDefinitionPair, controller);
    const { key, name, reader: read, writer: write } = definition;
    return {
        [name]: {
            get() {
                const value = this.data.get(key);
                if (value !== null) {
                    return read(value);
                }
                else {
                    return definition.defaultValue;
                }
            },
            set(value) {
                if (value === undefined) {
                    this.data.delete(key);
                }
                else {
                    this.data.set(key, write(value));
                }
            },
        },
        [`has${capitalize(name)}`]: {
            get() {
                return this.data.has(key) || definition.hasCustomDefaultValue;
            },
        },
    };
}
function parseValueDefinitionPair([token, typeDefinition], controller) {
    return valueDescriptorForTokenAndTypeDefinition({
        controller,
        token,
        typeDefinition,
    });
}
function parseValueTypeConstant(constant) {
    switch (constant) {
        case Array:
            return "array";
        case Boolean:
            return "boolean";
        case Number:
            return "number";
        case Object:
            return "object";
        case String:
            return "string";
    }
}
function parseValueTypeDefault(defaultValue) {
    switch (typeof defaultValue) {
        case "boolean":
            return "boolean";
        case "number":
            return "number";
        case "string":
            return "string";
    }
    if (Array.isArray(defaultValue))
        return "array";
    if (Object.prototype.toString.call(defaultValue) === "[object Object]")
        return "object";
}
function parseValueTypeObject(payload) {
    const { controller, token, typeObject } = payload;
    const hasType = isSomething(typeObject.type);
    const hasDefault = isSomething(typeObject.default);
    const fullObject = hasType && hasDefault;
    const onlyType = hasType && !hasDefault;
    const onlyDefault = !hasType && hasDefault;
    const typeFromObject = parseValueTypeConstant(typeObject.type);
    const typeFromDefaultValue = parseValueTypeDefault(payload.typeObject.default);
    if (onlyType)
        return typeFromObject;
    if (onlyDefault)
        return typeFromDefaultValue;
    if (typeFromObject !== typeFromDefaultValue) {
        const propertyPath = controller ? `${controller}.${token}` : token;
        throw new Error(`The specified default value for the Stimulus Value "${propertyPath}" must match the defined type "${typeFromObject}". The provided default value of "${typeObject.default}" is of type "${typeFromDefaultValue}".`);
    }
    if (fullObject)
        return typeFromObject;
}
function parseValueTypeDefinition(payload) {
    const { controller, token, typeDefinition } = payload;
    const typeObject = { controller, token, typeObject: typeDefinition };
    const typeFromObject = parseValueTypeObject(typeObject);
    const typeFromDefaultValue = parseValueTypeDefault(typeDefinition);
    const typeFromConstant = parseValueTypeConstant(typeDefinition);
    const type = typeFromObject || typeFromDefaultValue || typeFromConstant;
    if (type)
        return type;
    const propertyPath = controller ? `${controller}.${typeDefinition}` : token;
    throw new Error(`Unknown value type "${propertyPath}" for "${token}" value`);
}
function defaultValueForDefinition(typeDefinition) {
    const constant = parseValueTypeConstant(typeDefinition);
    if (constant)
        return defaultValuesByType[constant];
    const hasDefault = hasProperty(typeDefinition, "default");
    const hasType = hasProperty(typeDefinition, "type");
    const typeObject = typeDefinition;
    if (hasDefault)
        return typeObject.default;
    if (hasType) {
        const { type } = typeObject;
        const constantFromType = parseValueTypeConstant(type);
        if (constantFromType)
            return defaultValuesByType[constantFromType];
    }
    return typeDefinition;
}
function valueDescriptorForTokenAndTypeDefinition(payload) {
    const { token, typeDefinition } = payload;
    const key = `${dasherize(token)}-value`;
    const type = parseValueTypeDefinition(payload);
    return {
        type,
        key,
        name: camelize(key),
        get defaultValue() {
            return defaultValueForDefinition(typeDefinition);
        },
        get hasCustomDefaultValue() {
            return parseValueTypeDefault(typeDefinition) !== undefined;
        },
        reader: readers[type],
        writer: writers[type] || writers.default,
    };
}
const defaultValuesByType = {
    get array() {
        return [];
    },
    boolean: false,
    number: 0,
    get object() {
        return {};
    },
    string: "",
};
const readers = {
    array(value) {
        const array = JSON.parse(value);
        if (!Array.isArray(array)) {
            throw new TypeError(`expected value of type "array" but instead got value "${value}" of type "${parseValueTypeDefault(array)}"`);
        }
        return array;
    },
    boolean(value) {
        return !(value == "0" || String(value).toLowerCase() == "false");
    },
    number(value) {
        return Number(value.replace(/_/g, ""));
    },
    object(value) {
        const object = JSON.parse(value);
        if (object === null || typeof object != "object" || Array.isArray(object)) {
            throw new TypeError(`expected value of type "object" but instead got value "${value}" of type "${parseValueTypeDefault(object)}"`);
        }
        return object;
    },
    string(value) {
        return value;
    },
};
const writers = {
    default: writeString,
    array: writeJSON,
    object: writeJSON,
};
function writeJSON(value) {
    return JSON.stringify(value);
}
function writeString(value) {
    return `${value}`;
}

class Controller {
    constructor(context) {
        this.context = context;
    }
    static get shouldLoad() {
        return true;
    }
    static afterLoad(_identifier, _application) {
        return;
    }
    get application() {
        return this.context.application;
    }
    get scope() {
        return this.context.scope;
    }
    get element() {
        return this.scope.element;
    }
    get identifier() {
        return this.scope.identifier;
    }
    get targets() {
        return this.scope.targets;
    }
    get outlets() {
        return this.scope.outlets;
    }
    get classes() {
        return this.scope.classes;
    }
    get data() {
        return this.scope.data;
    }
    initialize() {
    }
    connect() {
    }
    disconnect() {
    }
    dispatch(eventName, { target = this.element, detail = {}, prefix = this.identifier, bubbles = true, cancelable = true, } = {}) {
        const type = prefix ? `${prefix}:${eventName}` : eventName;
        const event = new CustomEvent(type, { detail, bubbles, cancelable });
        target.dispatchEvent(event);
        return event;
    }
}
Controller.blessings = [
    ClassPropertiesBlessing,
    TargetPropertiesBlessing,
    ValuePropertiesBlessing,
    OutletPropertiesBlessing,
];
Controller.targets = [];
Controller.outlets = [];
Controller.values = {};



;// CONCATENATED MODULE: ./lib/stacks.ts

class StacksApplication extends Application {
    load(head, ...rest) {
        const definitions = Array.isArray(head) ? head : [head, ...rest];
        for (const definition of definitions) {
            const hasPrefix = /^s-/.test(definition.identifier);
            if (StacksApplication._initializing && !hasPrefix) {
                throw 'Stacks-created Stimulus controller names must start with "s-".';
            }
            if (!StacksApplication._initializing && hasPrefix) {
                throw 'The "s-" prefix on Stimulus controller names is reserved for Stacks-created controllers.';
            }
        }
        super.load(definitions);
    }
    static start(element, schema) {
        const application = new StacksApplication(element, schema);
        // eslint-disable-next-line @typescript-eslint/no-floating-promises
        application.start();
        return application;
    }
    static finalize() {
        StacksApplication._initializing = false;
    }
}
StacksApplication._initializing = true;
const application = StacksApplication.start();
class StacksController extends Controller {
    getElementData(element, key) {
        return element.getAttribute("data-" + this.identifier + "-" + key);
    }
    setElementData(element, key, value) {
        element.setAttribute("data-" + this.identifier + "-" + key, value);
    }
    removeElementData(element, key) {
        element.removeAttribute("data-" + this.identifier + "-" + key);
    }
    triggerEvent(eventName, detail, optionalElement) {
        const namespacedName = this.identifier + ":" + eventName;
        let event;
        try {
            event = new CustomEvent(namespacedName, {
                bubbles: true,
                cancelable: true,
                detail: detail,
            });
        }
        catch (ex) {
            // Internet Explorer
            // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
            event = document.createEvent("CustomEvent");
            event.initCustomEvent(namespacedName, true, true, detail);
        }
        (optionalElement || this.element).dispatchEvent(event);
        return event;
    }
}
function createController(controllerDefinition) {
    var _a;
    var _b;
    // eslint-disable-next-line no-prototype-builtins
    const Controller = controllerDefinition.hasOwnProperty("targets")
        ? (_b = class Controller extends StacksController {
            },
            _b.targets = (_a = controllerDefinition.targets) !== null && _a !== void 0 ? _a : [],
            _b) : class Controller extends StacksController {
    };
    for (const prop in controllerDefinition) {
        const ownPropDescriptor = 
        // eslint-disable-next-line no-prototype-builtins
        controllerDefinition.hasOwnProperty(prop) &&
            Object.getOwnPropertyDescriptor(controllerDefinition, prop);
        if (prop !== "targets" && ownPropDescriptor) {
            Object.defineProperty(Controller.prototype, prop, ownPropDescriptor);
        }
    }
    return Controller;
}
function addController(name, controller) {
    application.register(name, createController(controller));
}

;// CONCATENATED MODULE: ./lib/components/banner/banner.ts

class BannerController extends StacksController {
    /**
     * Toggles the visibility of the banner
     */
    toggle(dispatcher = null) {
        this._toggle(undefined, dispatcher);
    }
    /**
     * Shows the banner
     */
    show(dispatcher = null) {
        this._toggle(true, dispatcher);
    }
    /**
     * Hides the banner
     */
    hide(dispatcher = null) {
        this._toggle(false, dispatcher);
    }
    /**
     * Toggles the visibility of the banner element
     * @param show Optional parameter that force shows/hides the element or toggles it if left undefined
     */
    _toggle(show, dispatcher = null) {
        let toShow = show;
        const isVisible = this.bannerTarget.getAttribute("aria-hidden") === "false";
        // if we're letting the class toggle, we need to figure out if the banner is visible manually
        if (typeof toShow === "undefined") {
            toShow = !isVisible;
        }
        // if the state matches the disired state, return without changing anything
        if ((toShow && isVisible) || (!toShow && !isVisible)) {
            return;
        }
        const dispatchingElement = this.getDispatcher(dispatcher);
        // show/hide events trigger before toggling the class
        const triggeredEvent = this.triggerEvent(toShow ? "show" : "hide", {
            dispatcher: this.getDispatcher(dispatchingElement),
        }, this.bannerTarget);
        // if this pre-show/hide event was prevented, don't attempt to continue changing the banner state
        if (triggeredEvent.defaultPrevented) {
            return;
        }
        this.bannerTarget.setAttribute("aria-hidden", toShow ? "false" : "true");
        if (!toShow) {
            this.removeBannerOnHide();
        }
        this.triggerEvent(toShow ? "shown" : "hidden", {
            dispatcher: dispatchingElement,
        }, this.bannerTarget);
    }
    /**
     * Remove the element on hide if the `remove-when-hidden` flag is set
     */
    removeBannerOnHide() {
        if (this.data.get("remove-when-hidden") !== "true") {
            return;
        }
        this.bannerTarget.addEventListener("s-banner:hidden", () => {
            this.element.remove();
        }, { once: true });
    }
    /**
     * Determines the correct dispatching element from a potential input
     * @param dispatcher The event or element to get the dispatcher from
     */
    getDispatcher(dispatcher = null) {
        if (dispatcher instanceof Event) {
            return dispatcher.target;
        }
        else if (dispatcher instanceof Element) {
            return dispatcher;
        }
        else {
            return this.element;
        }
    }
}
BannerController.targets = ["banner"];
/**
 * Helper to manually show an s-banner element via external JS
 * @param element the element the `data-controller="s-banner"` attribute is on
 */
function showBanner(element) {
    toggleBanner(element, true);
}
/**
 * Helper to manually hide an s-banner element via external JS
 * @param element the element the `data-controller="s-banner"` attribute is on
 */
function hideBanner(element) {
    toggleBanner(element, false);
}
/**
 * Helper to manually show an s-banner element via external JS
 * @param element the element the `data-controller="s-banner"` attribute is on
 * @param show whether to force show/hide the banner; toggles the banner if left undefined
 */
function toggleBanner(element, show) {
    const controller = application.getControllerForElementAndIdentifier(element, "s-banner");
    if (!controller) {
        throw "Unable to get s-banner controller from element";
    }
    show ? controller.show() : controller.hide();
}

;// CONCATENATED MODULE: ./lib/components/expandable/expandable.ts

// Radio buttons only trigger a change event when they're *checked*, but not when
// they're *unchecked*. Therefore, if we have an active `s-expandable-control` in
// the document, we listen for change events on *all* radio buttons and find any
// other radio buttons in the same `name` group, triggering a custom event on all
// of them so the controller can re-evaluate.
//
// We're keeping a count of how many of these controllers are connected to the DOM,
// so only have this global listener when we actually need it.
const RADIO_OFF_EVENT = "s-expandable-control:radio-off";
function globalChangeListener(e) {
    const target = e.target;
    if (!(target instanceof HTMLInputElement) ||
        target.nodeName !== "INPUT" ||
        target.type !== "radio") {
        return;
    }
    document
        .querySelectorAll('input[type="radio"][name="' + target.name + '"]')
        .forEach(function (other) {
        if (other === e.target) {
            return;
        }
        let customEvent;
        try {
            customEvent = new Event(RADIO_OFF_EVENT);
        }
        catch (ex) {
            // Internet Explorer
            customEvent = document.createEvent("Event");
            customEvent.initEvent(RADIO_OFF_EVENT, true, true);
        }
        other.dispatchEvent(customEvent);
    });
}
let refCount = 0;
function globalChangeListenerRequired(required) {
    if (required) {
        refCount++;
        if (refCount === 1) {
            document.body.addEventListener("change", globalChangeListener);
        }
    }
    else {
        refCount--;
        if (refCount === 0) {
            document.body.removeEventListener("change", globalChangeListener);
        }
    }
}
class ExpandableController extends StacksController {
    constructor() {
        super(...arguments);
        this.lastKeydownClickTimestamp = 0;
    }
    initialize() {
        if (this.element.nodeName === "INPUT" &&
            ["radio", "checkbox"].indexOf(this.element.type) >= 0) {
            this.isCollapsed = this._isCollapsedForCheckable.bind(this);
            this.events = ["change", RADIO_OFF_EVENT];
            this.isCheckable = true;
            this.isRadio = this.element.type === "radio";
        }
        else {
            this.isCollapsed = this._isCollapsedForClickable.bind(this);
            this.events = ["click", "keydown"];
        }
        this.listener = this.listener.bind(this);
    }
    // for non-checkable elements, the initial source of truth is the collapsed/expanded
    // state of the controlled element (unless the element doesn't exist)
    _isCollapsedForClickable() {
        const cc = this.controlledExpandables;
        // the element is considered collapsed if *any* target element is collapsed
        return cc.length > 0
            ? !cc.every((element) => element.classList.contains("is-expanded"))
            : this.element.getAttribute("aria-expanded") === "false";
    }
    // for checkable elements, the initial source of truth is the checked state
    _isCollapsedForCheckable() {
        return !this.element.checked;
    }
    get controlledExpandables() {
        const attr = this.element.getAttribute("aria-controls");
        if (!attr) {
            throw `[aria-controls="targetId1 ... targetIdN"] attribute required`;
        }
        const result = attr
            .split(/\s+/g)
            .map((s) => document.getElementById(s))
            .filter((e) => !!e);
        if (!result.length) {
            throw "couldn't find controls";
        }
        return result;
    }
    _dispatchShowHideEvent(isShow) {
        this.triggerEvent(isShow ? "show" : "hide");
    }
    _toggleClass(doAdd) {
        if (!this.data.has("toggle-class")) {
            return;
        }
        const cl = this.element.classList;
        const toggleClass = this.data.get("toggle-class");
        if (!toggleClass) {
            throw "couldn't find toggle class";
        }
        toggleClass.split(/\s+/).forEach(function (cls) {
            cl.toggle(cls, !!doAdd);
        });
    }
    listener(e) {
        let newCollapsed;
        if (this.isCheckable) {
            newCollapsed = !this.element.checked;
        }
        else {
            if (e.type == "keydown" &&
                e instanceof KeyboardEvent &&
                e.keyCode != 13 &&
                e.keyCode != 32) {
                return;
            }
            if (e.target !== e.currentTarget &&
                ["A", "BUTTON"].indexOf(e.target.nodeName) >= 0) {
                return;
            }
            e.preventDefault();
            // Prevent "click" events from toggling the expandable within 300ms of "keydown".
            // e.preventDefault() should have done the same, but https://bugzilla.mozilla.org/show_bug.cgi?id=1487102
            // doesn't guarantee it.
            if (e.type == "keydown") {
                this.lastKeydownClickTimestamp = Date.now();
            }
            else if (e.type == "click" &&
                Date.now() - this.lastKeydownClickTimestamp < 300) {
                return;
            }
            newCollapsed =
                this.element.getAttribute("aria-expanded") === "true";
            if (e.type === "click") {
                this.element.blur();
            }
        }
        this.element.setAttribute("aria-expanded", newCollapsed ? "false" : "true");
        for (const controlledElement of this.controlledExpandables) {
            controlledElement.classList.toggle("is-expanded", !newCollapsed);
        }
        this._dispatchShowHideEvent(!newCollapsed);
        this._toggleClass(!newCollapsed);
    }
    connect() {
        this.events.forEach((e) => {
            this.element.addEventListener(e, this.listener.bind(this));
        }, this);
        if (this.isRadio) {
            globalChangeListenerRequired(true);
        }
        // synchronize state -- in all cases, this means setting the correct `aria-expanded`
        // attribute; for checkable controls this also means setting the `is-collapsed` class.
        // Note: aria-expanded is currently an invalid attribute on radio elements
        // Support for aria-expanded is being debated by the W3C https://github.com/w3c/aria/issues/1404 as recently as June 2022
        if (!this.isRadio) {
            this.element.setAttribute("aria-expanded", this.isCollapsed() ? "false" : "true");
        }
        if (this.isCheckable) {
            const cc = this.controlledExpandables;
            if (cc.length) {
                const expected = !this.isCollapsed();
                // if any element does not match the expected state, set them all to the expected state
                if (cc.some((element) => element.classList.contains("is-expanded") !==
                    expected)) {
                    for (const controlledElement of this
                        .controlledExpandables) {
                        controlledElement.classList.toggle("is-expanded", expected);
                    }
                    this._dispatchShowHideEvent(expected);
                    this._toggleClass(expected);
                }
            }
        }
    }
    disconnect() {
        this.events.forEach((e) => {
            this.element.removeEventListener(e, this.listener.bind(this));
        }, this);
        if (this.isRadio) {
            globalChangeListenerRequired(false);
        }
    }
}

;// CONCATENATED MODULE: ./lib/components/modal/modal.ts

class ModalController extends StacksController {
    connect() {
        this.validate();
    }
    /**
     * Disconnects all added event listeners on controller disconnect
     */
    disconnect() {
        this.unbindDocumentEvents();
    }
    /**
     * Toggles the visibility of the modal
     */
    toggle(dispatcher = null) {
        this._toggle(undefined, dispatcher);
    }
    /**
     * Shows the modal
     */
    show(dispatcher = null) {
        this._toggle(true, dispatcher);
    }
    /**
     * Hides the modal
     */
    hide(dispatcher = null) {
        this._toggle(false, dispatcher);
    }
    /**
     * Validates the modal settings and attempts to set necessary internal variables
     */
    validate() {
        // check for returnElement support
        const returnElementSelector = this.data.get("return-element");
        if (returnElementSelector) {
            this.returnElement = (document.querySelector(returnElementSelector));
            if (!this.returnElement) {
                throw ("Unable to find element by return-element selector: " +
                    returnElementSelector);
            }
        }
    }
    /**
     * Toggles the visibility of the modal element
     * @param show Optional parameter that force shows/hides the element or toggles it if left undefined
     */
    _toggle(show, dispatcher = null) {
        let toShow = show;
        const isVisible = this.modalTarget.getAttribute("aria-hidden") === "false";
        // if we're letting the class toggle, we need to figure out if the popover is visible manually
        if (typeof toShow === "undefined") {
            toShow = !isVisible;
        }
        // if the state matches the disired state, return without changing anything
        if ((toShow && isVisible) || (!toShow && !isVisible)) {
            return;
        }
        const dispatchingElement = this.getDispatcher(dispatcher);
        // show/hide events trigger before toggling the class
        const triggeredEvent = this.triggerEvent(toShow ? "show" : "hide", {
            returnElement: this.returnElement,
            dispatcher: this.getDispatcher(dispatchingElement),
        }, this.modalTarget);
        // if this pre-show/hide event was prevented, don't attempt to continue changing the modal state
        if (triggeredEvent.defaultPrevented) {
            return;
        }
        this.returnElement = triggeredEvent.detail.returnElement;
        this.modalTarget.setAttribute("aria-hidden", toShow ? "false" : "true");
        if (toShow) {
            this.bindDocumentEvents();
            this.focusInsideModal();
        }
        else {
            this.unbindDocumentEvents();
            this.focusReturnElement();
            this.removeModalOnHide();
        }
        // check for transitionend support
        const supportsTransitionEnd = this.modalTarget.ontransitionend !== undefined;
        // shown/hidden events trigger after toggling the class
        if (supportsTransitionEnd) {
            // wait until after the modal finishes transitioning to fire the event
            this.modalTarget.addEventListener("transitionend", () => {
                //TODO this is firing waaay to soon?
                this.triggerEvent(toShow ? "shown" : "hidden", {
                    dispatcher: dispatchingElement,
                }, this.modalTarget);
            }, { once: true });
        }
        else {
            this.triggerEvent(toShow ? "shown" : "hidden", {
                dispatcher: dispatchingElement,
            }, this.modalTarget);
        }
    }
    /**
     * Listens for the s-modal:hidden event and focuses the returnElement when it is fired
     */
    focusReturnElement() {
        if (!this.returnElement) {
            return;
        }
        this.modalTarget.addEventListener("s-modal:hidden", () => {
            // double check the element still exists when the event is called
            if (this.returnElement &&
                document.body.contains(this.returnElement)) {
                this.returnElement.focus();
            }
        }, { once: true });
    }
    /**
     * Remove the element on hide if the `remove-when-hidden` flag is set
     */
    removeModalOnHide() {
        if (this.data.get("remove-when-hidden") !== "true") {
            return;
        }
        this.modalTarget.addEventListener("s-modal:hidden", () => {
            this.element.remove();
        }, { once: true });
    }
    /**
     * Gets all elements within the modal that could receive keyboard focus.
     */
    getAllTabbables() {
        return Array.from(this.modalTarget.querySelectorAll("[href], input, select, textarea, button, [tabindex]")).filter((el) => el.matches(":not([disabled]):not([tabindex='-1'])"));
    }
    /**
     * Returns the first visible element in an array or `undefined` if no elements are visible.
     */
    firstVisible(elements) {
        // https://stackoverflow.com/a/21696585
        return elements.find((el) => el.offsetParent !== null);
    }
    /**
     * Returns the last visible element in an array or `undefined` if no elements are visible.
     */
    lastVisible(elements) {
        return this.firstVisible([...elements].reverse());
    }
    /**
     * Attempts to shift keyboard focus into the modal.
     * If elements with `data-s-modal-target="initialFocus"` are present and visible, one of those will be selected.
     * Otherwise, the first visible focusable element will receive focus.
     */
    focusInsideModal() {
        this.modalTarget.addEventListener("s-modal:shown", () => {
            var _a;
            const initialFocus = (_a = this.firstVisible(this.initialFocusTargets)) !== null && _a !== void 0 ? _a : this.firstVisible(this.getAllTabbables());
            // Only set focus if focus is not already set on an element within the modal
            if (!this.modalTarget.contains(document.activeElement)) {
                initialFocus === null || initialFocus === void 0 ? void 0 : initialFocus.focus();
            }
        }, { once: true });
    }
    /**
     * Returns keyboard focus to the modal if it has left or is about to leave.
     */
    keepFocusWithinModal(e) {
        // If somehow the user has tabbed out of the modal or if focus started outside the modal, push them to the first item.
        if (!this.modalTarget.contains(e.target)) {
            const focusTarget = this.firstVisible(this.getAllTabbables());
            if (focusTarget) {
                e.preventDefault();
                focusTarget.focus();
            }
            return;
        }
        // If we observe a tab keydown and we're on an edge, cycle the focus to the other side.
        if (e.key === "Tab") {
            const tabbables = this.getAllTabbables();
            const firstTabbable = this.firstVisible(tabbables);
            const lastTabbable = this.lastVisible(tabbables);
            if (firstTabbable && lastTabbable) {
                if (firstTabbable === lastTabbable) {
                    e.preventDefault();
                    firstTabbable.focus();
                }
                else if (e.shiftKey && e.target === firstTabbable) {
                    e.preventDefault();
                    lastTabbable.focus();
                }
                else if (!e.shiftKey && e.target === lastTabbable) {
                    e.preventDefault();
                    firstTabbable.focus();
                }
            }
        }
    }
    /**
     * Binds global events to the document for hiding popovers on user interaction
     */
    bindDocumentEvents() {
        // in order for removeEventListener to remove the right event, this bound function needs a constant reference
        this._boundClickFn =
            this._boundClickFn || this.hideOnOutsideClick.bind(this);
        this._boundKeypressFn =
            this._boundKeypressFn || this.hideOnEscapePress.bind(this);
        this._boundTabTrap =
            this._boundTabTrap || this.keepFocusWithinModal.bind(this);
        document.addEventListener("mousedown", this._boundClickFn);
        document.addEventListener("keyup", this._boundKeypressFn);
        document.addEventListener("keydown", this._boundTabTrap);
    }
    /**
     * Unbinds global events to the document for hiding popovers on user interaction
     */
    unbindDocumentEvents() {
        document.removeEventListener("mousedown", this._boundClickFn);
        document.removeEventListener("keyup", this._boundKeypressFn);
        document.removeEventListener("keydown", this._boundTabTrap);
    }
    /**
     * Forces the popover to hide if a user clicks outside of it or its reference element
     */
    hideOnOutsideClick(e) {
        var _a;
        const target = e.target;
        // check if the document was clicked inside either the toggle element or the modal itself
        // note: .contains also returns true if the node itself matches the target element
        if (!((_a = this.modalTarget
            .querySelector(".s-modal--dialog")) === null || _a === void 0 ? void 0 : _a.contains(target)) &&
            document.body.contains(target)) {
            this._toggle(false, e);
        }
    }
    /**
     * Forces the popover to hide if the user presses escape while it, one of its childen, or the reference element are focused
     */
    hideOnEscapePress(e) {
        // if the ESC key (27) wasn't pressed or if no popovers are showing, return
        if (e.which !== 27 ||
            this.modalTarget.getAttribute("aria-hidden") === "true") {
            return;
        }
        this._toggle(false, e);
    }
    /**
     * Determines the correct dispatching element from a potential input
     * @param dispatcher The event or element to get the dispatcher from
     */
    getDispatcher(dispatcher = null) {
        if (dispatcher instanceof Event) {
            return dispatcher.target;
        }
        else if (dispatcher instanceof Element) {
            return dispatcher;
        }
        else {
            return this.element;
        }
    }
}
ModalController.targets = ["modal", "initialFocus"];
/**
 * Helper to manually show an s-modal element via external JS
 * @param element the element the `data-controller="s-modal"` attribute is on
 */
function showModal(element) {
    toggleModal(element, true);
}
/**
 * Helper to manually hide an s-modal element via external JS
 * @param element the element the `data-controller="s-modal"` attribute is on
 */
function hideModal(element) {
    toggleModal(element, false);
}
/**
 * Helper to manually show an s-modal element via external JS
 * @param element the element the `data-controller="s-modal"` attribute is on
 * @param show whether to force show/hide the modal; toggles the modal if left undefined
 */
function toggleModal(element, show) {
    const controller = application.getControllerForElementAndIdentifier(element, "s-modal");
    if (!controller) {
        throw "Unable to get s-modal controller from element";
    }
    show ? controller.show() : controller.hide();
}

;// CONCATENATED MODULE: ./lib/components/navigation/navigation.ts

class TabListController extends StacksController {
    connect() {
        super.connect();
        this.boundSelectTab = this.selectTab.bind(this);
        this.boundHandleKeydown = this.handleKeydown.bind(this);
        for (const tab of this.tabTargets) {
            tab.addEventListener("click", this.boundSelectTab);
            tab.addEventListener("keydown", this.boundHandleKeydown);
        }
    }
    disconnect() {
        super.disconnect();
        for (const tab of this.tabTargets) {
            tab.removeEventListener("click", this.boundSelectTab);
            tab.removeEventListener("keydown", this.boundHandleKeydown);
        }
    }
    /**
     * Gets all tabs within the controller.
     */
    get tabTargets() {
        return Array.from(this.element.querySelectorAll("[role=tab]"));
    }
    /**
     * Handles click events on individual tabs, causing them to be selected.
     */
    selectTab(event) {
        this.switchToTab(event.currentTarget);
    }
    /**
     * Handles left and right arrow keydown events on individual tabs,
     * selecting the adjacent tab corresponding to the event.
     */
    handleKeydown(event) {
        var _a;
        let tabElement = event.currentTarget;
        const tabs = this.tabTargets;
        let tabIndex = tabs.indexOf(tabElement);
        if (event.key === "ArrowRight") {
            tabIndex++;
        }
        else if (event.key === "ArrowLeft") {
            tabIndex--;
        }
        else {
            return;
        }
        // Use circular navigation when users go past the first or last tab.
        if (tabIndex < 0) {
            tabIndex = tabs.length - 1;
        }
        if (tabIndex >= tabs.length) {
            tabIndex = 0;
        }
        tabElement = tabs[tabIndex];
        this.switchToTab(tabElement);
        // Focus the newly selected tab so it can receive keyboard events.
        (_a = this.selectedTab) === null || _a === void 0 ? void 0 : _a.focus();
    }
    /**
     * Attempts to switch to a new tab, doing nothing if the tab is already selected or
     * the s-navigation-tablist:select event is prevented.
     */
    switchToTab(newTab) {
        const oldTab = this.selectedTab;
        if (oldTab === newTab) {
            return;
        }
        if (this.triggerEvent("select", { oldTab, newTab }).defaultPrevented) {
            return;
        }
        this.selectedTab = newTab;
        this.triggerEvent("selected", { oldTab, newTab });
    }
    /**
     * Returns the currently selected tab or null if no tabs are selected.
     */
    get selectedTab() {
        return (this.tabTargets.find((e) => e.getAttribute("aria-selected") === "true") || null);
    }
    /**
     * Switches the tablist to the provided tab, updating the tabs and panels
     * to reflect the change.
     * @param selectedTab The tab to select. If `null` is provided or the element
     * is not a valid tab, all tabs will be unselected.
     */
    set selectedTab(selectedTab) {
        for (const tab of this.tabTargets) {
            const panelId = tab.getAttribute("aria-controls");
            const panel = panelId ? document.getElementById(panelId) : null;
            if (tab === selectedTab) {
                tab.classList.add("is-selected");
                tab.setAttribute("aria-selected", "true");
                tab.removeAttribute("tabindex");
                panel === null || panel === void 0 ? void 0 : panel.classList.remove("d-none");
            }
            else {
                tab.classList.remove("is-selected");
                tab.setAttribute("aria-selected", "false");
                tab.setAttribute("tabindex", "-1");
                panel === null || panel === void 0 ? void 0 : panel.classList.add("d-none");
            }
        }
    }
}

;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getWindow.js
function getWindow(node) {
  if (node == null) {
    return window;
  }

  if (node.toString() !== '[object Window]') {
    var ownerDocument = node.ownerDocument;
    return ownerDocument ? ownerDocument.defaultView || window : window;
  }

  return node;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/instanceOf.js


function isElement(node) {
  var OwnElement = getWindow(node).Element;
  return node instanceof OwnElement || node instanceof Element;
}

function isHTMLElement(node) {
  var OwnElement = getWindow(node).HTMLElement;
  return node instanceof OwnElement || node instanceof HTMLElement;
}

function isShadowRoot(node) {
  // IE 11 has no ShadowRoot
  if (typeof ShadowRoot === 'undefined') {
    return false;
  }

  var OwnElement = getWindow(node).ShadowRoot;
  return node instanceof OwnElement || node instanceof ShadowRoot;
}


;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/math.js
var math_max = Math.max;
var math_min = Math.min;
var round = Math.round;
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/userAgent.js
function getUAString() {
  var uaData = navigator.userAgentData;

  if (uaData != null && uaData.brands && Array.isArray(uaData.brands)) {
    return uaData.brands.map(function (item) {
      return item.brand + "/" + item.version;
    }).join(' ');
  }

  return navigator.userAgent;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/isLayoutViewport.js

function isLayoutViewport() {
  return !/^((?!chrome|android).)*safari/i.test(getUAString());
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getBoundingClientRect.js




function getBoundingClientRect(element, includeScale, isFixedStrategy) {
  if (includeScale === void 0) {
    includeScale = false;
  }

  if (isFixedStrategy === void 0) {
    isFixedStrategy = false;
  }

  var clientRect = element.getBoundingClientRect();
  var scaleX = 1;
  var scaleY = 1;

  if (includeScale && isHTMLElement(element)) {
    scaleX = element.offsetWidth > 0 ? round(clientRect.width) / element.offsetWidth || 1 : 1;
    scaleY = element.offsetHeight > 0 ? round(clientRect.height) / element.offsetHeight || 1 : 1;
  }

  var _ref = isElement(element) ? getWindow(element) : window,
      visualViewport = _ref.visualViewport;

  var addVisualOffsets = !isLayoutViewport() && isFixedStrategy;
  var x = (clientRect.left + (addVisualOffsets && visualViewport ? visualViewport.offsetLeft : 0)) / scaleX;
  var y = (clientRect.top + (addVisualOffsets && visualViewport ? visualViewport.offsetTop : 0)) / scaleY;
  var width = clientRect.width / scaleX;
  var height = clientRect.height / scaleY;
  return {
    width: width,
    height: height,
    top: y,
    right: x + width,
    bottom: y + height,
    left: x,
    x: x,
    y: y
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getWindowScroll.js

function getWindowScroll(node) {
  var win = getWindow(node);
  var scrollLeft = win.pageXOffset;
  var scrollTop = win.pageYOffset;
  return {
    scrollLeft: scrollLeft,
    scrollTop: scrollTop
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getHTMLElementScroll.js
function getHTMLElementScroll(element) {
  return {
    scrollLeft: element.scrollLeft,
    scrollTop: element.scrollTop
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getNodeScroll.js




function getNodeScroll(node) {
  if (node === getWindow(node) || !isHTMLElement(node)) {
    return getWindowScroll(node);
  } else {
    return getHTMLElementScroll(node);
  }
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getNodeName.js
function getNodeName(element) {
  return element ? (element.nodeName || '').toLowerCase() : null;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getDocumentElement.js

function getDocumentElement(element) {
  // $FlowFixMe[incompatible-return]: assume body is always available
  return ((isElement(element) ? element.ownerDocument : // $FlowFixMe[prop-missing]
  element.document) || window.document).documentElement;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getWindowScrollBarX.js



function getWindowScrollBarX(element) {
  // If <html> has a CSS width greater than the viewport, then this will be
  // incorrect for RTL.
  // Popper 1 is broken in this case and never had a bug report so let's assume
  // it's not an issue. I don't think anyone ever specifies width on <html>
  // anyway.
  // Browsers where the left scrollbar doesn't cause an issue report `0` for
  // this (e.g. Edge 2019, IE11, Safari)
  return getBoundingClientRect(getDocumentElement(element)).left + getWindowScroll(element).scrollLeft;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getComputedStyle.js

function getComputedStyle_getComputedStyle(element) {
  return getWindow(element).getComputedStyle(element);
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/isScrollParent.js

function isScrollParent(element) {
  // Firefox wants us to check `-x` and `-y` variations as well
  var _getComputedStyle = getComputedStyle_getComputedStyle(element),
      overflow = _getComputedStyle.overflow,
      overflowX = _getComputedStyle.overflowX,
      overflowY = _getComputedStyle.overflowY;

  return /auto|scroll|overlay|hidden/.test(overflow + overflowY + overflowX);
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getCompositeRect.js









function isElementScaled(element) {
  var rect = element.getBoundingClientRect();
  var scaleX = round(rect.width) / element.offsetWidth || 1;
  var scaleY = round(rect.height) / element.offsetHeight || 1;
  return scaleX !== 1 || scaleY !== 1;
} // Returns the composite rect of an element relative to its offsetParent.
// Composite means it takes into account transforms as well as layout.


function getCompositeRect(elementOrVirtualElement, offsetParent, isFixed) {
  if (isFixed === void 0) {
    isFixed = false;
  }

  var isOffsetParentAnElement = isHTMLElement(offsetParent);
  var offsetParentIsScaled = isHTMLElement(offsetParent) && isElementScaled(offsetParent);
  var documentElement = getDocumentElement(offsetParent);
  var rect = getBoundingClientRect(elementOrVirtualElement, offsetParentIsScaled, isFixed);
  var scroll = {
    scrollLeft: 0,
    scrollTop: 0
  };
  var offsets = {
    x: 0,
    y: 0
  };

  if (isOffsetParentAnElement || !isOffsetParentAnElement && !isFixed) {
    if (getNodeName(offsetParent) !== 'body' || // https://github.com/popperjs/popper-core/issues/1078
    isScrollParent(documentElement)) {
      scroll = getNodeScroll(offsetParent);
    }

    if (isHTMLElement(offsetParent)) {
      offsets = getBoundingClientRect(offsetParent, true);
      offsets.x += offsetParent.clientLeft;
      offsets.y += offsetParent.clientTop;
    } else if (documentElement) {
      offsets.x = getWindowScrollBarX(documentElement);
    }
  }

  return {
    x: rect.left + scroll.scrollLeft - offsets.x,
    y: rect.top + scroll.scrollTop - offsets.y,
    width: rect.width,
    height: rect.height
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getLayoutRect.js
 // Returns the layout rect of an element relative to its offsetParent. Layout
// means it doesn't take into account transforms.

function getLayoutRect(element) {
  var clientRect = getBoundingClientRect(element); // Use the clientRect sizes if it's not been transformed.
  // Fixes https://github.com/popperjs/popper-core/issues/1223

  var width = element.offsetWidth;
  var height = element.offsetHeight;

  if (Math.abs(clientRect.width - width) <= 1) {
    width = clientRect.width;
  }

  if (Math.abs(clientRect.height - height) <= 1) {
    height = clientRect.height;
  }

  return {
    x: element.offsetLeft,
    y: element.offsetTop,
    width: width,
    height: height
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getParentNode.js



function getParentNode(element) {
  if (getNodeName(element) === 'html') {
    return element;
  }

  return (// this is a quicker (but less type safe) way to save quite some bytes from the bundle
    // $FlowFixMe[incompatible-return]
    // $FlowFixMe[prop-missing]
    element.assignedSlot || // step into the shadow DOM of the parent of a slotted node
    element.parentNode || ( // DOM Element detected
    isShadowRoot(element) ? element.host : null) || // ShadowRoot detected
    // $FlowFixMe[incompatible-call]: HTMLElement is a Node
    getDocumentElement(element) // fallback

  );
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getScrollParent.js




function getScrollParent(node) {
  if (['html', 'body', '#document'].indexOf(getNodeName(node)) >= 0) {
    // $FlowFixMe[incompatible-return]: assume body is always available
    return node.ownerDocument.body;
  }

  if (isHTMLElement(node) && isScrollParent(node)) {
    return node;
  }

  return getScrollParent(getParentNode(node));
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/listScrollParents.js




/*
given a DOM element, return the list of all scroll parents, up the list of ancesors
until we get to the top window object. This list is what we attach scroll listeners
to, because if any of these parent elements scroll, we'll need to re-calculate the
reference element's position.
*/

function listScrollParents(element, list) {
  var _element$ownerDocumen;

  if (list === void 0) {
    list = [];
  }

  var scrollParent = getScrollParent(element);
  var isBody = scrollParent === ((_element$ownerDocumen = element.ownerDocument) == null ? void 0 : _element$ownerDocumen.body);
  var win = getWindow(scrollParent);
  var target = isBody ? [win].concat(win.visualViewport || [], isScrollParent(scrollParent) ? scrollParent : []) : scrollParent;
  var updatedList = list.concat(target);
  return isBody ? updatedList : // $FlowFixMe[incompatible-call]: isBody tells us target will be an HTMLElement here
  updatedList.concat(listScrollParents(getParentNode(target)));
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/isTableElement.js

function isTableElement(element) {
  return ['table', 'td', 'th'].indexOf(getNodeName(element)) >= 0;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getOffsetParent.js








function getTrueOffsetParent(element) {
  if (!isHTMLElement(element) || // https://github.com/popperjs/popper-core/issues/837
  getComputedStyle_getComputedStyle(element).position === 'fixed') {
    return null;
  }

  return element.offsetParent;
} // `.offsetParent` reports `null` for fixed elements, while absolute elements
// return the containing block


function getContainingBlock(element) {
  var isFirefox = /firefox/i.test(getUAString());
  var isIE = /Trident/i.test(getUAString());

  if (isIE && isHTMLElement(element)) {
    // In IE 9, 10 and 11 fixed elements containing block is always established by the viewport
    var elementCss = getComputedStyle_getComputedStyle(element);

    if (elementCss.position === 'fixed') {
      return null;
    }
  }

  var currentNode = getParentNode(element);

  if (isShadowRoot(currentNode)) {
    currentNode = currentNode.host;
  }

  while (isHTMLElement(currentNode) && ['html', 'body'].indexOf(getNodeName(currentNode)) < 0) {
    var css = getComputedStyle_getComputedStyle(currentNode); // This is non-exhaustive but covers the most common CSS properties that
    // create a containing block.
    // https://developer.mozilla.org/en-US/docs/Web/CSS/Containing_block#identifying_the_containing_block

    if (css.transform !== 'none' || css.perspective !== 'none' || css.contain === 'paint' || ['transform', 'perspective'].indexOf(css.willChange) !== -1 || isFirefox && css.willChange === 'filter' || isFirefox && css.filter && css.filter !== 'none') {
      return currentNode;
    } else {
      currentNode = currentNode.parentNode;
    }
  }

  return null;
} // Gets the closest ancestor positioned element. Handles some edge cases,
// such as table ancestors and cross browser bugs.


function getOffsetParent(element) {
  var window = getWindow(element);
  var offsetParent = getTrueOffsetParent(element);

  while (offsetParent && isTableElement(offsetParent) && getComputedStyle_getComputedStyle(offsetParent).position === 'static') {
    offsetParent = getTrueOffsetParent(offsetParent);
  }

  if (offsetParent && (getNodeName(offsetParent) === 'html' || getNodeName(offsetParent) === 'body' && getComputedStyle_getComputedStyle(offsetParent).position === 'static')) {
    return window;
  }

  return offsetParent || getContainingBlock(element) || window;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/enums.js
var enums_top = 'top';
var bottom = 'bottom';
var right = 'right';
var left = 'left';
var auto = 'auto';
var basePlacements = [enums_top, bottom, right, left];
var start = 'start';
var end = 'end';
var clippingParents = 'clippingParents';
var viewport = 'viewport';
var popper = 'popper';
var reference = 'reference';
var variationPlacements = /*#__PURE__*/basePlacements.reduce(function (acc, placement) {
  return acc.concat([placement + "-" + start, placement + "-" + end]);
}, []);
var enums_placements = /*#__PURE__*/[].concat(basePlacements, [auto]).reduce(function (acc, placement) {
  return acc.concat([placement, placement + "-" + start, placement + "-" + end]);
}, []); // modifiers that need to read the DOM

var beforeRead = 'beforeRead';
var read = 'read';
var afterRead = 'afterRead'; // pure-logic modifiers

var beforeMain = 'beforeMain';
var main = 'main';
var afterMain = 'afterMain'; // modifier with the purpose to write to the DOM (or write into a framework state)

var beforeWrite = 'beforeWrite';
var write = 'write';
var afterWrite = 'afterWrite';
var modifierPhases = [beforeRead, read, afterRead, beforeMain, main, afterMain, beforeWrite, write, afterWrite];
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/orderModifiers.js
 // source: https://stackoverflow.com/questions/49875255

function order(modifiers) {
  var map = new Map();
  var visited = new Set();
  var result = [];
  modifiers.forEach(function (modifier) {
    map.set(modifier.name, modifier);
  }); // On visiting object, check for its dependencies and visit them recursively

  function sort(modifier) {
    visited.add(modifier.name);
    var requires = [].concat(modifier.requires || [], modifier.requiresIfExists || []);
    requires.forEach(function (dep) {
      if (!visited.has(dep)) {
        var depModifier = map.get(dep);

        if (depModifier) {
          sort(depModifier);
        }
      }
    });
    result.push(modifier);
  }

  modifiers.forEach(function (modifier) {
    if (!visited.has(modifier.name)) {
      // check for visited object
      sort(modifier);
    }
  });
  return result;
}

function orderModifiers(modifiers) {
  // order based on dependencies
  var orderedModifiers = order(modifiers); // order based on phase

  return modifierPhases.reduce(function (acc, phase) {
    return acc.concat(orderedModifiers.filter(function (modifier) {
      return modifier.phase === phase;
    }));
  }, []);
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/debounce.js
function debounce(fn) {
  var pending;
  return function () {
    if (!pending) {
      pending = new Promise(function (resolve) {
        Promise.resolve().then(function () {
          pending = undefined;
          resolve(fn());
        });
      });
    }

    return pending;
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/mergeByName.js
function mergeByName(modifiers) {
  var merged = modifiers.reduce(function (merged, current) {
    var existing = merged[current.name];
    merged[current.name] = existing ? Object.assign({}, existing, current, {
      options: Object.assign({}, existing.options, current.options),
      data: Object.assign({}, existing.data, current.data)
    }) : current;
    return merged;
  }, {}); // IE11 does not support Object.values

  return Object.keys(merged).map(function (key) {
    return merged[key];
  });
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/createPopper.js









var DEFAULT_OPTIONS = {
  placement: 'bottom',
  modifiers: [],
  strategy: 'absolute'
};

function areValidElements() {
  for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
    args[_key] = arguments[_key];
  }

  return !args.some(function (element) {
    return !(element && typeof element.getBoundingClientRect === 'function');
  });
}

function popperGenerator(generatorOptions) {
  if (generatorOptions === void 0) {
    generatorOptions = {};
  }

  var _generatorOptions = generatorOptions,
      _generatorOptions$def = _generatorOptions.defaultModifiers,
      defaultModifiers = _generatorOptions$def === void 0 ? [] : _generatorOptions$def,
      _generatorOptions$def2 = _generatorOptions.defaultOptions,
      defaultOptions = _generatorOptions$def2 === void 0 ? DEFAULT_OPTIONS : _generatorOptions$def2;
  return function createPopper(reference, popper, options) {
    if (options === void 0) {
      options = defaultOptions;
    }

    var state = {
      placement: 'bottom',
      orderedModifiers: [],
      options: Object.assign({}, DEFAULT_OPTIONS, defaultOptions),
      modifiersData: {},
      elements: {
        reference: reference,
        popper: popper
      },
      attributes: {},
      styles: {}
    };
    var effectCleanupFns = [];
    var isDestroyed = false;
    var instance = {
      state: state,
      setOptions: function setOptions(setOptionsAction) {
        var options = typeof setOptionsAction === 'function' ? setOptionsAction(state.options) : setOptionsAction;
        cleanupModifierEffects();
        state.options = Object.assign({}, defaultOptions, state.options, options);
        state.scrollParents = {
          reference: isElement(reference) ? listScrollParents(reference) : reference.contextElement ? listScrollParents(reference.contextElement) : [],
          popper: listScrollParents(popper)
        }; // Orders the modifiers based on their dependencies and `phase`
        // properties

        var orderedModifiers = orderModifiers(mergeByName([].concat(defaultModifiers, state.options.modifiers))); // Strip out disabled modifiers

        state.orderedModifiers = orderedModifiers.filter(function (m) {
          return m.enabled;
        });
        runModifierEffects();
        return instance.update();
      },
      // Sync update – it will always be executed, even if not necessary. This
      // is useful for low frequency updates where sync behavior simplifies the
      // logic.
      // For high frequency updates (e.g. `resize` and `scroll` events), always
      // prefer the async Popper#update method
      forceUpdate: function forceUpdate() {
        if (isDestroyed) {
          return;
        }

        var _state$elements = state.elements,
            reference = _state$elements.reference,
            popper = _state$elements.popper; // Don't proceed if `reference` or `popper` are not valid elements
        // anymore

        if (!areValidElements(reference, popper)) {
          return;
        } // Store the reference and popper rects to be read by modifiers


        state.rects = {
          reference: getCompositeRect(reference, getOffsetParent(popper), state.options.strategy === 'fixed'),
          popper: getLayoutRect(popper)
        }; // Modifiers have the ability to reset the current update cycle. The
        // most common use case for this is the `flip` modifier changing the
        // placement, which then needs to re-run all the modifiers, because the
        // logic was previously ran for the previous placement and is therefore
        // stale/incorrect

        state.reset = false;
        state.placement = state.options.placement; // On each update cycle, the `modifiersData` property for each modifier
        // is filled with the initial data specified by the modifier. This means
        // it doesn't persist and is fresh on each update.
        // To ensure persistent data, use `${name}#persistent`

        state.orderedModifiers.forEach(function (modifier) {
          return state.modifiersData[modifier.name] = Object.assign({}, modifier.data);
        });

        for (var index = 0; index < state.orderedModifiers.length; index++) {
          if (state.reset === true) {
            state.reset = false;
            index = -1;
            continue;
          }

          var _state$orderedModifie = state.orderedModifiers[index],
              fn = _state$orderedModifie.fn,
              _state$orderedModifie2 = _state$orderedModifie.options,
              _options = _state$orderedModifie2 === void 0 ? {} : _state$orderedModifie2,
              name = _state$orderedModifie.name;

          if (typeof fn === 'function') {
            state = fn({
              state: state,
              options: _options,
              name: name,
              instance: instance
            }) || state;
          }
        }
      },
      // Async and optimistically optimized update – it will not be executed if
      // not necessary (debounced to run at most once-per-tick)
      update: debounce(function () {
        return new Promise(function (resolve) {
          instance.forceUpdate();
          resolve(state);
        });
      }),
      destroy: function destroy() {
        cleanupModifierEffects();
        isDestroyed = true;
      }
    };

    if (!areValidElements(reference, popper)) {
      return instance;
    }

    instance.setOptions(options).then(function (state) {
      if (!isDestroyed && options.onFirstUpdate) {
        options.onFirstUpdate(state);
      }
    }); // Modifiers have the ability to execute arbitrary code before the first
    // update cycle runs. They will be executed in the same order as the update
    // cycle. This is useful when a modifier adds some persistent data that
    // other modifiers need to use, but the modifier is run after the dependent
    // one.

    function runModifierEffects() {
      state.orderedModifiers.forEach(function (_ref) {
        var name = _ref.name,
            _ref$options = _ref.options,
            options = _ref$options === void 0 ? {} : _ref$options,
            effect = _ref.effect;

        if (typeof effect === 'function') {
          var cleanupFn = effect({
            state: state,
            name: name,
            instance: instance,
            options: options
          });

          var noopFn = function noopFn() {};

          effectCleanupFns.push(cleanupFn || noopFn);
        }
      });
    }

    function cleanupModifierEffects() {
      effectCleanupFns.forEach(function (fn) {
        return fn();
      });
      effectCleanupFns = [];
    }

    return instance;
  };
}
var createPopper = /*#__PURE__*/(/* unused pure expression or super */ null && (popperGenerator())); // eslint-disable-next-line import/no-unused-modules


;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/eventListeners.js
 // eslint-disable-next-line import/no-unused-modules

var passive = {
  passive: true
};

function effect(_ref) {
  var state = _ref.state,
      instance = _ref.instance,
      options = _ref.options;
  var _options$scroll = options.scroll,
      scroll = _options$scroll === void 0 ? true : _options$scroll,
      _options$resize = options.resize,
      resize = _options$resize === void 0 ? true : _options$resize;
  var window = getWindow(state.elements.popper);
  var scrollParents = [].concat(state.scrollParents.reference, state.scrollParents.popper);

  if (scroll) {
    scrollParents.forEach(function (scrollParent) {
      scrollParent.addEventListener('scroll', instance.update, passive);
    });
  }

  if (resize) {
    window.addEventListener('resize', instance.update, passive);
  }

  return function () {
    if (scroll) {
      scrollParents.forEach(function (scrollParent) {
        scrollParent.removeEventListener('scroll', instance.update, passive);
      });
    }

    if (resize) {
      window.removeEventListener('resize', instance.update, passive);
    }
  };
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const eventListeners = ({
  name: 'eventListeners',
  enabled: true,
  phase: 'write',
  fn: function fn() {},
  effect: effect,
  data: {}
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getBasePlacement.js

function getBasePlacement(placement) {
  return placement.split('-')[0];
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getVariation.js
function getVariation(placement) {
  return placement.split('-')[1];
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getMainAxisFromPlacement.js
function getMainAxisFromPlacement(placement) {
  return ['top', 'bottom'].indexOf(placement) >= 0 ? 'x' : 'y';
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/computeOffsets.js




function computeOffsets(_ref) {
  var reference = _ref.reference,
      element = _ref.element,
      placement = _ref.placement;
  var basePlacement = placement ? getBasePlacement(placement) : null;
  var variation = placement ? getVariation(placement) : null;
  var commonX = reference.x + reference.width / 2 - element.width / 2;
  var commonY = reference.y + reference.height / 2 - element.height / 2;
  var offsets;

  switch (basePlacement) {
    case enums_top:
      offsets = {
        x: commonX,
        y: reference.y - element.height
      };
      break;

    case bottom:
      offsets = {
        x: commonX,
        y: reference.y + reference.height
      };
      break;

    case right:
      offsets = {
        x: reference.x + reference.width,
        y: commonY
      };
      break;

    case left:
      offsets = {
        x: reference.x - element.width,
        y: commonY
      };
      break;

    default:
      offsets = {
        x: reference.x,
        y: reference.y
      };
  }

  var mainAxis = basePlacement ? getMainAxisFromPlacement(basePlacement) : null;

  if (mainAxis != null) {
    var len = mainAxis === 'y' ? 'height' : 'width';

    switch (variation) {
      case start:
        offsets[mainAxis] = offsets[mainAxis] - (reference[len] / 2 - element[len] / 2);
        break;

      case end:
        offsets[mainAxis] = offsets[mainAxis] + (reference[len] / 2 - element[len] / 2);
        break;

      default:
    }
  }

  return offsets;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/popperOffsets.js


function popperOffsets(_ref) {
  var state = _ref.state,
      name = _ref.name;
  // Offsets are the actual position the popper needs to have to be
  // properly positioned near its reference element
  // This is the most basic placement, and will be adjusted by
  // the modifiers in the next step
  state.modifiersData[name] = computeOffsets({
    reference: state.rects.reference,
    element: state.rects.popper,
    strategy: 'absolute',
    placement: state.placement
  });
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_popperOffsets = ({
  name: 'popperOffsets',
  enabled: true,
  phase: 'read',
  fn: popperOffsets,
  data: {}
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/computeStyles.js







 // eslint-disable-next-line import/no-unused-modules

var unsetSides = {
  top: 'auto',
  right: 'auto',
  bottom: 'auto',
  left: 'auto'
}; // Round the offsets to the nearest suitable subpixel based on the DPR.
// Zooming can change the DPR, but it seems to report a value that will
// cleanly divide the values into the appropriate subpixels.

function roundOffsetsByDPR(_ref, win) {
  var x = _ref.x,
      y = _ref.y;
  var dpr = win.devicePixelRatio || 1;
  return {
    x: round(x * dpr) / dpr || 0,
    y: round(y * dpr) / dpr || 0
  };
}

function mapToStyles(_ref2) {
  var _Object$assign2;

  var popper = _ref2.popper,
      popperRect = _ref2.popperRect,
      placement = _ref2.placement,
      variation = _ref2.variation,
      offsets = _ref2.offsets,
      position = _ref2.position,
      gpuAcceleration = _ref2.gpuAcceleration,
      adaptive = _ref2.adaptive,
      roundOffsets = _ref2.roundOffsets,
      isFixed = _ref2.isFixed;
  var _offsets$x = offsets.x,
      x = _offsets$x === void 0 ? 0 : _offsets$x,
      _offsets$y = offsets.y,
      y = _offsets$y === void 0 ? 0 : _offsets$y;

  var _ref3 = typeof roundOffsets === 'function' ? roundOffsets({
    x: x,
    y: y
  }) : {
    x: x,
    y: y
  };

  x = _ref3.x;
  y = _ref3.y;
  var hasX = offsets.hasOwnProperty('x');
  var hasY = offsets.hasOwnProperty('y');
  var sideX = left;
  var sideY = enums_top;
  var win = window;

  if (adaptive) {
    var offsetParent = getOffsetParent(popper);
    var heightProp = 'clientHeight';
    var widthProp = 'clientWidth';

    if (offsetParent === getWindow(popper)) {
      offsetParent = getDocumentElement(popper);

      if (getComputedStyle_getComputedStyle(offsetParent).position !== 'static' && position === 'absolute') {
        heightProp = 'scrollHeight';
        widthProp = 'scrollWidth';
      }
    } // $FlowFixMe[incompatible-cast]: force type refinement, we compare offsetParent with window above, but Flow doesn't detect it


    offsetParent = offsetParent;

    if (placement === enums_top || (placement === left || placement === right) && variation === end) {
      sideY = bottom;
      var offsetY = isFixed && offsetParent === win && win.visualViewport ? win.visualViewport.height : // $FlowFixMe[prop-missing]
      offsetParent[heightProp];
      y -= offsetY - popperRect.height;
      y *= gpuAcceleration ? 1 : -1;
    }

    if (placement === left || (placement === enums_top || placement === bottom) && variation === end) {
      sideX = right;
      var offsetX = isFixed && offsetParent === win && win.visualViewport ? win.visualViewport.width : // $FlowFixMe[prop-missing]
      offsetParent[widthProp];
      x -= offsetX - popperRect.width;
      x *= gpuAcceleration ? 1 : -1;
    }
  }

  var commonStyles = Object.assign({
    position: position
  }, adaptive && unsetSides);

  var _ref4 = roundOffsets === true ? roundOffsetsByDPR({
    x: x,
    y: y
  }, getWindow(popper)) : {
    x: x,
    y: y
  };

  x = _ref4.x;
  y = _ref4.y;

  if (gpuAcceleration) {
    var _Object$assign;

    return Object.assign({}, commonStyles, (_Object$assign = {}, _Object$assign[sideY] = hasY ? '0' : '', _Object$assign[sideX] = hasX ? '0' : '', _Object$assign.transform = (win.devicePixelRatio || 1) <= 1 ? "translate(" + x + "px, " + y + "px)" : "translate3d(" + x + "px, " + y + "px, 0)", _Object$assign));
  }

  return Object.assign({}, commonStyles, (_Object$assign2 = {}, _Object$assign2[sideY] = hasY ? y + "px" : '', _Object$assign2[sideX] = hasX ? x + "px" : '', _Object$assign2.transform = '', _Object$assign2));
}

function computeStyles(_ref5) {
  var state = _ref5.state,
      options = _ref5.options;
  var _options$gpuAccelerat = options.gpuAcceleration,
      gpuAcceleration = _options$gpuAccelerat === void 0 ? true : _options$gpuAccelerat,
      _options$adaptive = options.adaptive,
      adaptive = _options$adaptive === void 0 ? true : _options$adaptive,
      _options$roundOffsets = options.roundOffsets,
      roundOffsets = _options$roundOffsets === void 0 ? true : _options$roundOffsets;
  var commonStyles = {
    placement: getBasePlacement(state.placement),
    variation: getVariation(state.placement),
    popper: state.elements.popper,
    popperRect: state.rects.popper,
    gpuAcceleration: gpuAcceleration,
    isFixed: state.options.strategy === 'fixed'
  };

  if (state.modifiersData.popperOffsets != null) {
    state.styles.popper = Object.assign({}, state.styles.popper, mapToStyles(Object.assign({}, commonStyles, {
      offsets: state.modifiersData.popperOffsets,
      position: state.options.strategy,
      adaptive: adaptive,
      roundOffsets: roundOffsets
    })));
  }

  if (state.modifiersData.arrow != null) {
    state.styles.arrow = Object.assign({}, state.styles.arrow, mapToStyles(Object.assign({}, commonStyles, {
      offsets: state.modifiersData.arrow,
      position: 'absolute',
      adaptive: false,
      roundOffsets: roundOffsets
    })));
  }

  state.attributes.popper = Object.assign({}, state.attributes.popper, {
    'data-popper-placement': state.placement
  });
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_computeStyles = ({
  name: 'computeStyles',
  enabled: true,
  phase: 'beforeWrite',
  fn: computeStyles,
  data: {}
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/applyStyles.js

 // This modifier takes the styles prepared by the `computeStyles` modifier
// and applies them to the HTMLElements such as popper and arrow

function applyStyles(_ref) {
  var state = _ref.state;
  Object.keys(state.elements).forEach(function (name) {
    var style = state.styles[name] || {};
    var attributes = state.attributes[name] || {};
    var element = state.elements[name]; // arrow is optional + virtual elements

    if (!isHTMLElement(element) || !getNodeName(element)) {
      return;
    } // Flow doesn't support to extend this property, but it's the most
    // effective way to apply styles to an HTMLElement
    // $FlowFixMe[cannot-write]


    Object.assign(element.style, style);
    Object.keys(attributes).forEach(function (name) {
      var value = attributes[name];

      if (value === false) {
        element.removeAttribute(name);
      } else {
        element.setAttribute(name, value === true ? '' : value);
      }
    });
  });
}

function applyStyles_effect(_ref2) {
  var state = _ref2.state;
  var initialStyles = {
    popper: {
      position: state.options.strategy,
      left: '0',
      top: '0',
      margin: '0'
    },
    arrow: {
      position: 'absolute'
    },
    reference: {}
  };
  Object.assign(state.elements.popper.style, initialStyles.popper);
  state.styles = initialStyles;

  if (state.elements.arrow) {
    Object.assign(state.elements.arrow.style, initialStyles.arrow);
  }

  return function () {
    Object.keys(state.elements).forEach(function (name) {
      var element = state.elements[name];
      var attributes = state.attributes[name] || {};
      var styleProperties = Object.keys(state.styles.hasOwnProperty(name) ? state.styles[name] : initialStyles[name]); // Set all values to an empty string to unset them

      var style = styleProperties.reduce(function (style, property) {
        style[property] = '';
        return style;
      }, {}); // arrow is optional + virtual elements

      if (!isHTMLElement(element) || !getNodeName(element)) {
        return;
      }

      Object.assign(element.style, style);
      Object.keys(attributes).forEach(function (attribute) {
        element.removeAttribute(attribute);
      });
    });
  };
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_applyStyles = ({
  name: 'applyStyles',
  enabled: true,
  phase: 'write',
  fn: applyStyles,
  effect: applyStyles_effect,
  requires: ['computeStyles']
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/offset.js

 // eslint-disable-next-line import/no-unused-modules

function distanceAndSkiddingToXY(placement, rects, offset) {
  var basePlacement = getBasePlacement(placement);
  var invertDistance = [left, enums_top].indexOf(basePlacement) >= 0 ? -1 : 1;

  var _ref = typeof offset === 'function' ? offset(Object.assign({}, rects, {
    placement: placement
  })) : offset,
      skidding = _ref[0],
      distance = _ref[1];

  skidding = skidding || 0;
  distance = (distance || 0) * invertDistance;
  return [left, right].indexOf(basePlacement) >= 0 ? {
    x: distance,
    y: skidding
  } : {
    x: skidding,
    y: distance
  };
}

function offset(_ref2) {
  var state = _ref2.state,
      options = _ref2.options,
      name = _ref2.name;
  var _options$offset = options.offset,
      offset = _options$offset === void 0 ? [0, 0] : _options$offset;
  var data = enums_placements.reduce(function (acc, placement) {
    acc[placement] = distanceAndSkiddingToXY(placement, state.rects, offset);
    return acc;
  }, {});
  var _data$state$placement = data[state.placement],
      x = _data$state$placement.x,
      y = _data$state$placement.y;

  if (state.modifiersData.popperOffsets != null) {
    state.modifiersData.popperOffsets.x += x;
    state.modifiersData.popperOffsets.y += y;
  }

  state.modifiersData[name] = data;
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_offset = ({
  name: 'offset',
  enabled: true,
  phase: 'main',
  requires: ['popperOffsets'],
  fn: offset
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getOppositePlacement.js
var hash = {
  left: 'right',
  right: 'left',
  bottom: 'top',
  top: 'bottom'
};
function getOppositePlacement(placement) {
  return placement.replace(/left|right|bottom|top/g, function (matched) {
    return hash[matched];
  });
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getOppositeVariationPlacement.js
var getOppositeVariationPlacement_hash = {
  start: 'end',
  end: 'start'
};
function getOppositeVariationPlacement(placement) {
  return placement.replace(/start|end/g, function (matched) {
    return getOppositeVariationPlacement_hash[matched];
  });
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getViewportRect.js




function getViewportRect(element, strategy) {
  var win = getWindow(element);
  var html = getDocumentElement(element);
  var visualViewport = win.visualViewport;
  var width = html.clientWidth;
  var height = html.clientHeight;
  var x = 0;
  var y = 0;

  if (visualViewport) {
    width = visualViewport.width;
    height = visualViewport.height;
    var layoutViewport = isLayoutViewport();

    if (layoutViewport || !layoutViewport && strategy === 'fixed') {
      x = visualViewport.offsetLeft;
      y = visualViewport.offsetTop;
    }
  }

  return {
    width: width,
    height: height,
    x: x + getWindowScrollBarX(element),
    y: y
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getDocumentRect.js




 // Gets the entire size of the scrollable document area, even extending outside
// of the `<html>` and `<body>` rect bounds if horizontally scrollable

function getDocumentRect(element) {
  var _element$ownerDocumen;

  var html = getDocumentElement(element);
  var winScroll = getWindowScroll(element);
  var body = (_element$ownerDocumen = element.ownerDocument) == null ? void 0 : _element$ownerDocumen.body;
  var width = math_max(html.scrollWidth, html.clientWidth, body ? body.scrollWidth : 0, body ? body.clientWidth : 0);
  var height = math_max(html.scrollHeight, html.clientHeight, body ? body.scrollHeight : 0, body ? body.clientHeight : 0);
  var x = -winScroll.scrollLeft + getWindowScrollBarX(element);
  var y = -winScroll.scrollTop;

  if (getComputedStyle_getComputedStyle(body || html).direction === 'rtl') {
    x += math_max(html.clientWidth, body ? body.clientWidth : 0) - width;
  }

  return {
    width: width,
    height: height,
    x: x,
    y: y
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/contains.js

function contains(parent, child) {
  var rootNode = child.getRootNode && child.getRootNode(); // First, attempt with faster native method

  if (parent.contains(child)) {
    return true;
  } // then fallback to custom implementation with Shadow DOM support
  else if (rootNode && isShadowRoot(rootNode)) {
      var next = child;

      do {
        if (next && parent.isSameNode(next)) {
          return true;
        } // $FlowFixMe[prop-missing]: need a better way to handle this...


        next = next.parentNode || next.host;
      } while (next);
    } // Give up, the result is false


  return false;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/rectToClientRect.js
function rectToClientRect(rect) {
  return Object.assign({}, rect, {
    left: rect.x,
    top: rect.y,
    right: rect.x + rect.width,
    bottom: rect.y + rect.height
  });
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/dom-utils/getClippingRect.js















function getInnerBoundingClientRect(element, strategy) {
  var rect = getBoundingClientRect(element, false, strategy === 'fixed');
  rect.top = rect.top + element.clientTop;
  rect.left = rect.left + element.clientLeft;
  rect.bottom = rect.top + element.clientHeight;
  rect.right = rect.left + element.clientWidth;
  rect.width = element.clientWidth;
  rect.height = element.clientHeight;
  rect.x = rect.left;
  rect.y = rect.top;
  return rect;
}

function getClientRectFromMixedType(element, clippingParent, strategy) {
  return clippingParent === viewport ? rectToClientRect(getViewportRect(element, strategy)) : isElement(clippingParent) ? getInnerBoundingClientRect(clippingParent, strategy) : rectToClientRect(getDocumentRect(getDocumentElement(element)));
} // A "clipping parent" is an overflowable container with the characteristic of
// clipping (or hiding) overflowing elements with a position different from
// `initial`


function getClippingParents(element) {
  var clippingParents = listScrollParents(getParentNode(element));
  var canEscapeClipping = ['absolute', 'fixed'].indexOf(getComputedStyle_getComputedStyle(element).position) >= 0;
  var clipperElement = canEscapeClipping && isHTMLElement(element) ? getOffsetParent(element) : element;

  if (!isElement(clipperElement)) {
    return [];
  } // $FlowFixMe[incompatible-return]: https://github.com/facebook/flow/issues/1414


  return clippingParents.filter(function (clippingParent) {
    return isElement(clippingParent) && contains(clippingParent, clipperElement) && getNodeName(clippingParent) !== 'body';
  });
} // Gets the maximum area that the element is visible in due to any number of
// clipping parents


function getClippingRect(element, boundary, rootBoundary, strategy) {
  var mainClippingParents = boundary === 'clippingParents' ? getClippingParents(element) : [].concat(boundary);
  var clippingParents = [].concat(mainClippingParents, [rootBoundary]);
  var firstClippingParent = clippingParents[0];
  var clippingRect = clippingParents.reduce(function (accRect, clippingParent) {
    var rect = getClientRectFromMixedType(element, clippingParent, strategy);
    accRect.top = math_max(rect.top, accRect.top);
    accRect.right = math_min(rect.right, accRect.right);
    accRect.bottom = math_min(rect.bottom, accRect.bottom);
    accRect.left = math_max(rect.left, accRect.left);
    return accRect;
  }, getClientRectFromMixedType(element, firstClippingParent, strategy));
  clippingRect.width = clippingRect.right - clippingRect.left;
  clippingRect.height = clippingRect.bottom - clippingRect.top;
  clippingRect.x = clippingRect.left;
  clippingRect.y = clippingRect.top;
  return clippingRect;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getFreshSideObject.js
function getFreshSideObject() {
  return {
    top: 0,
    right: 0,
    bottom: 0,
    left: 0
  };
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/mergePaddingObject.js

function mergePaddingObject(paddingObject) {
  return Object.assign({}, getFreshSideObject(), paddingObject);
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/expandToHashMap.js
function expandToHashMap(value, keys) {
  return keys.reduce(function (hashMap, key) {
    hashMap[key] = value;
    return hashMap;
  }, {});
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/detectOverflow.js








 // eslint-disable-next-line import/no-unused-modules

function detectOverflow(state, options) {
  if (options === void 0) {
    options = {};
  }

  var _options = options,
      _options$placement = _options.placement,
      placement = _options$placement === void 0 ? state.placement : _options$placement,
      _options$strategy = _options.strategy,
      strategy = _options$strategy === void 0 ? state.strategy : _options$strategy,
      _options$boundary = _options.boundary,
      boundary = _options$boundary === void 0 ? clippingParents : _options$boundary,
      _options$rootBoundary = _options.rootBoundary,
      rootBoundary = _options$rootBoundary === void 0 ? viewport : _options$rootBoundary,
      _options$elementConte = _options.elementContext,
      elementContext = _options$elementConte === void 0 ? popper : _options$elementConte,
      _options$altBoundary = _options.altBoundary,
      altBoundary = _options$altBoundary === void 0 ? false : _options$altBoundary,
      _options$padding = _options.padding,
      padding = _options$padding === void 0 ? 0 : _options$padding;
  var paddingObject = mergePaddingObject(typeof padding !== 'number' ? padding : expandToHashMap(padding, basePlacements));
  var altContext = elementContext === popper ? reference : popper;
  var popperRect = state.rects.popper;
  var element = state.elements[altBoundary ? altContext : elementContext];
  var clippingClientRect = getClippingRect(isElement(element) ? element : element.contextElement || getDocumentElement(state.elements.popper), boundary, rootBoundary, strategy);
  var referenceClientRect = getBoundingClientRect(state.elements.reference);
  var popperOffsets = computeOffsets({
    reference: referenceClientRect,
    element: popperRect,
    strategy: 'absolute',
    placement: placement
  });
  var popperClientRect = rectToClientRect(Object.assign({}, popperRect, popperOffsets));
  var elementClientRect = elementContext === popper ? popperClientRect : referenceClientRect; // positive = overflowing the clipping rect
  // 0 or negative = within the clipping rect

  var overflowOffsets = {
    top: clippingClientRect.top - elementClientRect.top + paddingObject.top,
    bottom: elementClientRect.bottom - clippingClientRect.bottom + paddingObject.bottom,
    left: clippingClientRect.left - elementClientRect.left + paddingObject.left,
    right: elementClientRect.right - clippingClientRect.right + paddingObject.right
  };
  var offsetData = state.modifiersData.offset; // Offsets can be applied only to the popper element

  if (elementContext === popper && offsetData) {
    var offset = offsetData[placement];
    Object.keys(overflowOffsets).forEach(function (key) {
      var multiply = [right, bottom].indexOf(key) >= 0 ? 1 : -1;
      var axis = [enums_top, bottom].indexOf(key) >= 0 ? 'y' : 'x';
      overflowOffsets[key] += offset[axis] * multiply;
    });
  }

  return overflowOffsets;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/computeAutoPlacement.js




function computeAutoPlacement(state, options) {
  if (options === void 0) {
    options = {};
  }

  var _options = options,
      placement = _options.placement,
      boundary = _options.boundary,
      rootBoundary = _options.rootBoundary,
      padding = _options.padding,
      flipVariations = _options.flipVariations,
      _options$allowedAutoP = _options.allowedAutoPlacements,
      allowedAutoPlacements = _options$allowedAutoP === void 0 ? enums_placements : _options$allowedAutoP;
  var variation = getVariation(placement);
  var placements = variation ? flipVariations ? variationPlacements : variationPlacements.filter(function (placement) {
    return getVariation(placement) === variation;
  }) : basePlacements;
  var allowedPlacements = placements.filter(function (placement) {
    return allowedAutoPlacements.indexOf(placement) >= 0;
  });

  if (allowedPlacements.length === 0) {
    allowedPlacements = placements;
  } // $FlowFixMe[incompatible-type]: Flow seems to have problems with two array unions...


  var overflows = allowedPlacements.reduce(function (acc, placement) {
    acc[placement] = detectOverflow(state, {
      placement: placement,
      boundary: boundary,
      rootBoundary: rootBoundary,
      padding: padding
    })[getBasePlacement(placement)];
    return acc;
  }, {});
  return Object.keys(overflows).sort(function (a, b) {
    return overflows[a] - overflows[b];
  });
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/flip.js






 // eslint-disable-next-line import/no-unused-modules

function getExpandedFallbackPlacements(placement) {
  if (getBasePlacement(placement) === auto) {
    return [];
  }

  var oppositePlacement = getOppositePlacement(placement);
  return [getOppositeVariationPlacement(placement), oppositePlacement, getOppositeVariationPlacement(oppositePlacement)];
}

function flip(_ref) {
  var state = _ref.state,
      options = _ref.options,
      name = _ref.name;

  if (state.modifiersData[name]._skip) {
    return;
  }

  var _options$mainAxis = options.mainAxis,
      checkMainAxis = _options$mainAxis === void 0 ? true : _options$mainAxis,
      _options$altAxis = options.altAxis,
      checkAltAxis = _options$altAxis === void 0 ? true : _options$altAxis,
      specifiedFallbackPlacements = options.fallbackPlacements,
      padding = options.padding,
      boundary = options.boundary,
      rootBoundary = options.rootBoundary,
      altBoundary = options.altBoundary,
      _options$flipVariatio = options.flipVariations,
      flipVariations = _options$flipVariatio === void 0 ? true : _options$flipVariatio,
      allowedAutoPlacements = options.allowedAutoPlacements;
  var preferredPlacement = state.options.placement;
  var basePlacement = getBasePlacement(preferredPlacement);
  var isBasePlacement = basePlacement === preferredPlacement;
  var fallbackPlacements = specifiedFallbackPlacements || (isBasePlacement || !flipVariations ? [getOppositePlacement(preferredPlacement)] : getExpandedFallbackPlacements(preferredPlacement));
  var placements = [preferredPlacement].concat(fallbackPlacements).reduce(function (acc, placement) {
    return acc.concat(getBasePlacement(placement) === auto ? computeAutoPlacement(state, {
      placement: placement,
      boundary: boundary,
      rootBoundary: rootBoundary,
      padding: padding,
      flipVariations: flipVariations,
      allowedAutoPlacements: allowedAutoPlacements
    }) : placement);
  }, []);
  var referenceRect = state.rects.reference;
  var popperRect = state.rects.popper;
  var checksMap = new Map();
  var makeFallbackChecks = true;
  var firstFittingPlacement = placements[0];

  for (var i = 0; i < placements.length; i++) {
    var placement = placements[i];

    var _basePlacement = getBasePlacement(placement);

    var isStartVariation = getVariation(placement) === start;
    var isVertical = [enums_top, bottom].indexOf(_basePlacement) >= 0;
    var len = isVertical ? 'width' : 'height';
    var overflow = detectOverflow(state, {
      placement: placement,
      boundary: boundary,
      rootBoundary: rootBoundary,
      altBoundary: altBoundary,
      padding: padding
    });
    var mainVariationSide = isVertical ? isStartVariation ? right : left : isStartVariation ? bottom : enums_top;

    if (referenceRect[len] > popperRect[len]) {
      mainVariationSide = getOppositePlacement(mainVariationSide);
    }

    var altVariationSide = getOppositePlacement(mainVariationSide);
    var checks = [];

    if (checkMainAxis) {
      checks.push(overflow[_basePlacement] <= 0);
    }

    if (checkAltAxis) {
      checks.push(overflow[mainVariationSide] <= 0, overflow[altVariationSide] <= 0);
    }

    if (checks.every(function (check) {
      return check;
    })) {
      firstFittingPlacement = placement;
      makeFallbackChecks = false;
      break;
    }

    checksMap.set(placement, checks);
  }

  if (makeFallbackChecks) {
    // `2` may be desired in some cases – research later
    var numberOfChecks = flipVariations ? 3 : 1;

    var _loop = function _loop(_i) {
      var fittingPlacement = placements.find(function (placement) {
        var checks = checksMap.get(placement);

        if (checks) {
          return checks.slice(0, _i).every(function (check) {
            return check;
          });
        }
      });

      if (fittingPlacement) {
        firstFittingPlacement = fittingPlacement;
        return "break";
      }
    };

    for (var _i = numberOfChecks; _i > 0; _i--) {
      var _ret = _loop(_i);

      if (_ret === "break") break;
    }
  }

  if (state.placement !== firstFittingPlacement) {
    state.modifiersData[name]._skip = true;
    state.placement = firstFittingPlacement;
    state.reset = true;
  }
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_flip = ({
  name: 'flip',
  enabled: true,
  phase: 'main',
  fn: flip,
  requiresIfExists: ['offset'],
  data: {
    _skip: false
  }
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/getAltAxis.js
function getAltAxis(axis) {
  return axis === 'x' ? 'y' : 'x';
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/utils/within.js

function within(min, value, max) {
  return math_max(min, math_min(value, max));
}
function withinMaxClamp(min, value, max) {
  var v = within(min, value, max);
  return v > max ? max : v;
}
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/preventOverflow.js












function preventOverflow(_ref) {
  var state = _ref.state,
      options = _ref.options,
      name = _ref.name;
  var _options$mainAxis = options.mainAxis,
      checkMainAxis = _options$mainAxis === void 0 ? true : _options$mainAxis,
      _options$altAxis = options.altAxis,
      checkAltAxis = _options$altAxis === void 0 ? false : _options$altAxis,
      boundary = options.boundary,
      rootBoundary = options.rootBoundary,
      altBoundary = options.altBoundary,
      padding = options.padding,
      _options$tether = options.tether,
      tether = _options$tether === void 0 ? true : _options$tether,
      _options$tetherOffset = options.tetherOffset,
      tetherOffset = _options$tetherOffset === void 0 ? 0 : _options$tetherOffset;
  var overflow = detectOverflow(state, {
    boundary: boundary,
    rootBoundary: rootBoundary,
    padding: padding,
    altBoundary: altBoundary
  });
  var basePlacement = getBasePlacement(state.placement);
  var variation = getVariation(state.placement);
  var isBasePlacement = !variation;
  var mainAxis = getMainAxisFromPlacement(basePlacement);
  var altAxis = getAltAxis(mainAxis);
  var popperOffsets = state.modifiersData.popperOffsets;
  var referenceRect = state.rects.reference;
  var popperRect = state.rects.popper;
  var tetherOffsetValue = typeof tetherOffset === 'function' ? tetherOffset(Object.assign({}, state.rects, {
    placement: state.placement
  })) : tetherOffset;
  var normalizedTetherOffsetValue = typeof tetherOffsetValue === 'number' ? {
    mainAxis: tetherOffsetValue,
    altAxis: tetherOffsetValue
  } : Object.assign({
    mainAxis: 0,
    altAxis: 0
  }, tetherOffsetValue);
  var offsetModifierState = state.modifiersData.offset ? state.modifiersData.offset[state.placement] : null;
  var data = {
    x: 0,
    y: 0
  };

  if (!popperOffsets) {
    return;
  }

  if (checkMainAxis) {
    var _offsetModifierState$;

    var mainSide = mainAxis === 'y' ? enums_top : left;
    var altSide = mainAxis === 'y' ? bottom : right;
    var len = mainAxis === 'y' ? 'height' : 'width';
    var offset = popperOffsets[mainAxis];
    var min = offset + overflow[mainSide];
    var max = offset - overflow[altSide];
    var additive = tether ? -popperRect[len] / 2 : 0;
    var minLen = variation === start ? referenceRect[len] : popperRect[len];
    var maxLen = variation === start ? -popperRect[len] : -referenceRect[len]; // We need to include the arrow in the calculation so the arrow doesn't go
    // outside the reference bounds

    var arrowElement = state.elements.arrow;
    var arrowRect = tether && arrowElement ? getLayoutRect(arrowElement) : {
      width: 0,
      height: 0
    };
    var arrowPaddingObject = state.modifiersData['arrow#persistent'] ? state.modifiersData['arrow#persistent'].padding : getFreshSideObject();
    var arrowPaddingMin = arrowPaddingObject[mainSide];
    var arrowPaddingMax = arrowPaddingObject[altSide]; // If the reference length is smaller than the arrow length, we don't want
    // to include its full size in the calculation. If the reference is small
    // and near the edge of a boundary, the popper can overflow even if the
    // reference is not overflowing as well (e.g. virtual elements with no
    // width or height)

    var arrowLen = within(0, referenceRect[len], arrowRect[len]);
    var minOffset = isBasePlacement ? referenceRect[len] / 2 - additive - arrowLen - arrowPaddingMin - normalizedTetherOffsetValue.mainAxis : minLen - arrowLen - arrowPaddingMin - normalizedTetherOffsetValue.mainAxis;
    var maxOffset = isBasePlacement ? -referenceRect[len] / 2 + additive + arrowLen + arrowPaddingMax + normalizedTetherOffsetValue.mainAxis : maxLen + arrowLen + arrowPaddingMax + normalizedTetherOffsetValue.mainAxis;
    var arrowOffsetParent = state.elements.arrow && getOffsetParent(state.elements.arrow);
    var clientOffset = arrowOffsetParent ? mainAxis === 'y' ? arrowOffsetParent.clientTop || 0 : arrowOffsetParent.clientLeft || 0 : 0;
    var offsetModifierValue = (_offsetModifierState$ = offsetModifierState == null ? void 0 : offsetModifierState[mainAxis]) != null ? _offsetModifierState$ : 0;
    var tetherMin = offset + minOffset - offsetModifierValue - clientOffset;
    var tetherMax = offset + maxOffset - offsetModifierValue;
    var preventedOffset = within(tether ? math_min(min, tetherMin) : min, offset, tether ? math_max(max, tetherMax) : max);
    popperOffsets[mainAxis] = preventedOffset;
    data[mainAxis] = preventedOffset - offset;
  }

  if (checkAltAxis) {
    var _offsetModifierState$2;

    var _mainSide = mainAxis === 'x' ? enums_top : left;

    var _altSide = mainAxis === 'x' ? bottom : right;

    var _offset = popperOffsets[altAxis];

    var _len = altAxis === 'y' ? 'height' : 'width';

    var _min = _offset + overflow[_mainSide];

    var _max = _offset - overflow[_altSide];

    var isOriginSide = [enums_top, left].indexOf(basePlacement) !== -1;

    var _offsetModifierValue = (_offsetModifierState$2 = offsetModifierState == null ? void 0 : offsetModifierState[altAxis]) != null ? _offsetModifierState$2 : 0;

    var _tetherMin = isOriginSide ? _min : _offset - referenceRect[_len] - popperRect[_len] - _offsetModifierValue + normalizedTetherOffsetValue.altAxis;

    var _tetherMax = isOriginSide ? _offset + referenceRect[_len] + popperRect[_len] - _offsetModifierValue - normalizedTetherOffsetValue.altAxis : _max;

    var _preventedOffset = tether && isOriginSide ? withinMaxClamp(_tetherMin, _offset, _tetherMax) : within(tether ? _tetherMin : _min, _offset, tether ? _tetherMax : _max);

    popperOffsets[altAxis] = _preventedOffset;
    data[altAxis] = _preventedOffset - _offset;
  }

  state.modifiersData[name] = data;
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_preventOverflow = ({
  name: 'preventOverflow',
  enabled: true,
  phase: 'main',
  fn: preventOverflow,
  requiresIfExists: ['offset']
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/arrow.js








 // eslint-disable-next-line import/no-unused-modules

var toPaddingObject = function toPaddingObject(padding, state) {
  padding = typeof padding === 'function' ? padding(Object.assign({}, state.rects, {
    placement: state.placement
  })) : padding;
  return mergePaddingObject(typeof padding !== 'number' ? padding : expandToHashMap(padding, basePlacements));
};

function arrow(_ref) {
  var _state$modifiersData$;

  var state = _ref.state,
      name = _ref.name,
      options = _ref.options;
  var arrowElement = state.elements.arrow;
  var popperOffsets = state.modifiersData.popperOffsets;
  var basePlacement = getBasePlacement(state.placement);
  var axis = getMainAxisFromPlacement(basePlacement);
  var isVertical = [left, right].indexOf(basePlacement) >= 0;
  var len = isVertical ? 'height' : 'width';

  if (!arrowElement || !popperOffsets) {
    return;
  }

  var paddingObject = toPaddingObject(options.padding, state);
  var arrowRect = getLayoutRect(arrowElement);
  var minProp = axis === 'y' ? enums_top : left;
  var maxProp = axis === 'y' ? bottom : right;
  var endDiff = state.rects.reference[len] + state.rects.reference[axis] - popperOffsets[axis] - state.rects.popper[len];
  var startDiff = popperOffsets[axis] - state.rects.reference[axis];
  var arrowOffsetParent = getOffsetParent(arrowElement);
  var clientSize = arrowOffsetParent ? axis === 'y' ? arrowOffsetParent.clientHeight || 0 : arrowOffsetParent.clientWidth || 0 : 0;
  var centerToReference = endDiff / 2 - startDiff / 2; // Make sure the arrow doesn't overflow the popper if the center point is
  // outside of the popper bounds

  var min = paddingObject[minProp];
  var max = clientSize - arrowRect[len] - paddingObject[maxProp];
  var center = clientSize / 2 - arrowRect[len] / 2 + centerToReference;
  var offset = within(min, center, max); // Prevents breaking syntax highlighting...

  var axisProp = axis;
  state.modifiersData[name] = (_state$modifiersData$ = {}, _state$modifiersData$[axisProp] = offset, _state$modifiersData$.centerOffset = offset - center, _state$modifiersData$);
}

function arrow_effect(_ref2) {
  var state = _ref2.state,
      options = _ref2.options;
  var _options$element = options.element,
      arrowElement = _options$element === void 0 ? '[data-popper-arrow]' : _options$element;

  if (arrowElement == null) {
    return;
  } // CSS selector


  if (typeof arrowElement === 'string') {
    arrowElement = state.elements.popper.querySelector(arrowElement);

    if (!arrowElement) {
      return;
    }
  }

  if (!contains(state.elements.popper, arrowElement)) {
    return;
  }

  state.elements.arrow = arrowElement;
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_arrow = ({
  name: 'arrow',
  enabled: true,
  phase: 'main',
  fn: arrow,
  effect: arrow_effect,
  requires: ['popperOffsets'],
  requiresIfExists: ['preventOverflow']
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/modifiers/hide.js



function getSideOffsets(overflow, rect, preventedOffsets) {
  if (preventedOffsets === void 0) {
    preventedOffsets = {
      x: 0,
      y: 0
    };
  }

  return {
    top: overflow.top - rect.height - preventedOffsets.y,
    right: overflow.right - rect.width + preventedOffsets.x,
    bottom: overflow.bottom - rect.height + preventedOffsets.y,
    left: overflow.left - rect.width - preventedOffsets.x
  };
}

function isAnySideFullyClipped(overflow) {
  return [enums_top, right, bottom, left].some(function (side) {
    return overflow[side] >= 0;
  });
}

function hide(_ref) {
  var state = _ref.state,
      name = _ref.name;
  var referenceRect = state.rects.reference;
  var popperRect = state.rects.popper;
  var preventedOffsets = state.modifiersData.preventOverflow;
  var referenceOverflow = detectOverflow(state, {
    elementContext: 'reference'
  });
  var popperAltOverflow = detectOverflow(state, {
    altBoundary: true
  });
  var referenceClippingOffsets = getSideOffsets(referenceOverflow, referenceRect);
  var popperEscapeOffsets = getSideOffsets(popperAltOverflow, popperRect, preventedOffsets);
  var isReferenceHidden = isAnySideFullyClipped(referenceClippingOffsets);
  var hasPopperEscaped = isAnySideFullyClipped(popperEscapeOffsets);
  state.modifiersData[name] = {
    referenceClippingOffsets: referenceClippingOffsets,
    popperEscapeOffsets: popperEscapeOffsets,
    isReferenceHidden: isReferenceHidden,
    hasPopperEscaped: hasPopperEscaped
  };
  state.attributes.popper = Object.assign({}, state.attributes.popper, {
    'data-popper-reference-hidden': isReferenceHidden,
    'data-popper-escaped': hasPopperEscaped
  });
} // eslint-disable-next-line import/no-unused-modules


/* harmony default export */ const modifiers_hide = ({
  name: 'hide',
  enabled: true,
  phase: 'main',
  requiresIfExists: ['preventOverflow'],
  fn: hide
});
;// CONCATENATED MODULE: ./node_modules/@popperjs/core/lib/popper.js










var defaultModifiers = [eventListeners, modifiers_popperOffsets, modifiers_computeStyles, modifiers_applyStyles, modifiers_offset, modifiers_flip, modifiers_preventOverflow, modifiers_arrow, modifiers_hide];
var popper_createPopper = /*#__PURE__*/popperGenerator({
  defaultModifiers: defaultModifiers
}); // eslint-disable-next-line import/no-unused-modules

 // eslint-disable-next-line import/no-unused-modules

 // eslint-disable-next-line import/no-unused-modules


;// CONCATENATED MODULE: ./lib/components/popover/popover.ts


class BasePopoverController extends StacksController {
    /**
     * Returns true if the if the popover is currently visible.
     */
    get isVisible() {
        const popoverElement = this.popoverElement;
        return popoverElement
            ? popoverElement.classList.contains("is-visible")
            : false;
    }
    /**
     * Gets whether the element is visible in the browser's viewport.
     */
    get isInViewport() {
        const element = this.popoverElement;
        if (!this.isVisible || !element) {
            return false;
        }
        // From https://stackoverflow.com/a/5354536.  Theoretically, this could be calculated using Popper's detectOverflow function,
        // but it's unclear how to access that with our current configuration.
        const rect = element.getBoundingClientRect();
        const viewHeight = Math.max(document.documentElement.clientHeight, window.innerHeight);
        const viewWidth = Math.max(document.documentElement.clientWidth, window.innerWidth);
        return (rect.bottom > 0 &&
            rect.top < viewHeight &&
            rect.right > 0 &&
            rect.left < viewWidth);
    }
    get shouldHideOnOutsideClick() {
        const hideBehavior = (this.data.get("hide-on-outside-click"));
        switch (hideBehavior) {
            case "after-dismissal":
            case "never":
                return false;
            case "if-in-viewport":
                return this.isInViewport;
            default:
                return true;
        }
    }
    /**
     * Initializes and validates controller variables
     */
    connect() {
        super.connect();
        this.validate();
        if (this.isVisible) {
            // just call initialize here, not show. This keeps already visible popovers from adding/firing document events
            this.initializePopper();
        }
        else if (this.data.get("auto-show") === "true") {
            this.show(null);
        }
        this.data.delete("auto-show");
    }
    /**
     * Cleans up popper.js elements and disconnects all added event listeners
     */
    disconnect() {
        this.hide();
        if (this.popper) {
            this.popper.destroy();
            // eslint-disable-next-line
            // @ts-ignore The operand of a 'delete' operator must be optional .ts(2790)
            delete this.popper;
        }
        super.disconnect();
    }
    /**
     * Toggles the visibility of the popover
     */
    toggle(dispatcher = null) {
        this.isVisible ? this.hide(dispatcher) : this.show(dispatcher);
    }
    /**
     * Shows the popover if not already visible
     */
    show(dispatcher = null) {
        if (this.isVisible) {
            return;
        }
        const dispatcherElement = this.getDispatcher(dispatcher);
        if (this.triggerEvent("show", {
            dispatcher: dispatcherElement,
        }).defaultPrevented) {
            return;
        }
        if (!this.popper) {
            this.initializePopper();
        }
        this.popoverElement.classList.add("is-visible");
        // ensure the popper has been positioned correctly
        this.scheduleUpdate();
        this.shown(dispatcherElement);
    }
    /**
     * Hides the popover if not already hidden
     */
    hide(dispatcher = null) {
        if (!this.isVisible) {
            return;
        }
        const dispatcherElement = this.getDispatcher(dispatcher);
        if (this.triggerEvent("hide", {
            dispatcher: dispatcherElement,
        }).defaultPrevented) {
            return;
        }
        this.popoverElement.classList.remove("is-visible");
        if (this.popper) {
            // completely destroy the popper on hide; this is in line with Popper.js's performance recommendations
            this.popper.destroy();
            // eslint-disable-next-line
            // @ts-ignore The operand of a 'delete' operator must be optional .ts(2790)
            delete this.popper;
        }
        // on first interaction, hide-on-outside-click with value "after-dismissal" reverts to the default behavior
        if (this.data.get("hide-on-outside-click") ===
            "after-dismissal") {
            this.data.delete("hide-on-outside-click");
        }
        this.hidden(dispatcherElement);
    }
    /**
     * Binds document events for this popover and fires the shown event
     */
    shown(dispatcher = null) {
        this.bindDocumentEvents();
        this.triggerEvent("shown", {
            dispatcher: dispatcher,
        });
    }
    /**
     * Unbinds document events for this popover and fires the hidden event
     */
    hidden(dispatcher = null) {
        this.unbindDocumentEvents();
        this.triggerEvent("hidden", {
            dispatcher: dispatcher,
        });
    }
    /**
     * Generates the popover if not found during initialization
     */
    generatePopover() {
        return null;
    }
    /**
     * Initializes the Popper for this instance
     */
    initializePopper() {
        this.popper = popper_createPopper(this.referenceElement, this.popoverElement, {
            placement: this.data.get("placement") || "bottom",
            modifiers: [
                {
                    name: "offset",
                    options: {
                        offset: [0, 10], // The entire popover should be 10px away from the element
                    },
                },
                {
                    name: "arrow",
                    options: {
                        element: ".s-popover--arrow",
                    },
                },
            ],
        });
    }
    /**
     * Validates the popover settings and attempts to set necessary internal variables
     */
    validate() {
        const referenceSelector = this.data.get("reference-selector");
        this.referenceElement = this.element;
        // if there is an alternative reference selector and that element exists, use it (and throw if it isn't found)
        if (referenceSelector) {
            this.referenceElement = (this.element.querySelector(referenceSelector));
            if (!this.referenceElement) {
                throw ("Unable to find element by reference selector: " +
                    referenceSelector);
            }
        }
        const popoverId = this.referenceElement.getAttribute(this.popoverSelectorAttribute);
        let popoverElement = null;
        // if the popover is named, attempt to fetch it (and throw an error if it doesn't exist)
        if (popoverId) {
            popoverElement = document.getElementById(popoverId);
            if (!popoverElement) {
                throw `[${this.popoverSelectorAttribute}="{POPOVER_ID}"] required`;
            }
        }
        // if the popover isn't named, attempt to generate it
        else {
            popoverElement = this.generatePopover();
        }
        if (!popoverElement) {
            throw "unable to find or generate popover element";
        }
        this.popoverElement = popoverElement;
    }
    /**
     * Determines the correct dispatching element from a potential input
     * @param dispatcher The event or element to get the dispatcher from
     */
    getDispatcher(dispatcher = null) {
        if (dispatcher instanceof Event) {
            return dispatcher.target;
        }
        else if (dispatcher instanceof Element) {
            return dispatcher;
        }
        else {
            return this.element;
        }
    }
    /**
     * Schedules the popover to update on the next animation frame if visible
     */
    scheduleUpdate() {
        if (this.popper && this.isVisible) {
            void this.popper.update();
        }
    }
}
class PopoverController extends BasePopoverController {
    constructor() {
        super(...arguments);
        this.popoverSelectorAttribute = "aria-controls";
    }
    /**
     * Toggles optional classes and accessibility attributes in addition to BasePopoverController.shown
     */
    shown(dispatcher = null) {
        this.toggleOptionalClasses(true);
        this.toggleAccessibilityAttributes(true);
        super.shown(dispatcher);
    }
    /**
     * Toggles optional classes and accessibility attributes in addition to BasePopoverController.hidden
     */
    hidden(dispatcher = null) {
        this.toggleOptionalClasses(false);
        this.toggleAccessibilityAttributes(false);
        super.hidden(dispatcher);
    }
    /**
     * Initializes accessibility attributes in addition to BasePopoverController.connect
     */
    connect() {
        super.connect();
        this.toggleAccessibilityAttributes();
    }
    /**
     * Binds global events to the document for hiding popovers on user interaction
     */
    bindDocumentEvents() {
        this.boundHideOnOutsideClick =
            this.boundHideOnOutsideClick || this.hideOnOutsideClick.bind(this);
        this.boundHideOnEscapePress =
            this.boundHideOnEscapePress || this.hideOnEscapePress.bind(this);
        document.addEventListener("mousedown", this.boundHideOnOutsideClick);
        document.addEventListener("keyup", this.boundHideOnEscapePress);
    }
    /**
     * Unbinds global events to the document for hiding popovers on user interaction
     */
    unbindDocumentEvents() {
        document.removeEventListener("mousedown", this.boundHideOnOutsideClick);
        document.removeEventListener("keyup", this.boundHideOnEscapePress);
    }
    /**
     * Forces the popover to hide if a user clicks outside of it or its reference element
     * @param {Event} e - The document click event
     */
    hideOnOutsideClick(e) {
        const target = e.target;
        // check if the document was clicked inside either the reference element or the popover itself
        // note: .contains also returns true if the node itself matches the target element
        if (this.shouldHideOnOutsideClick &&
            !this.referenceElement.contains(target) &&
            !this.popoverElement.contains(target) &&
            document.body.contains(target)) {
            this.hide(e);
        }
    }
    /**
     * Forces the popover to hide if the user presses escape while it, one of its childen, or the reference element are focused
     * @param {Event} e - The document keyup event
     */
    hideOnEscapePress(e) {
        // if the ESC key (27) wasn't pressed or if no popovers are showing, return
        if (e.which !== 27 || !this.isVisible) {
            return;
        }
        // check if the target was inside the popover element and refocus the triggering element
        // note: .contains also returns true if the node itself matches the target element
        if (this.popoverElement.contains(e.target)) {
            this.referenceElement.focus();
        }
        this.hide(e);
    }
    /**
     * Toggles all classes on the originating element based on the `class-toggle` data
     * @param {boolean=} show - A boolean indicating whether this is being triggered by a show or hide.
     */
    toggleOptionalClasses(show) {
        if (!this.data.has("toggle-class")) {
            return;
        }
        const toggleClass = this.data.get("toggle-class") || "";
        const cl = this.referenceElement.classList;
        toggleClass.split(/\s+/).forEach(function (cls) {
            cl.toggle(cls, show);
        });
    }
    /**
     * Toggles accessibility attributes based on whether the popover is shown or not
     * @param {boolean=} show - A boolean indicating whether this is being triggered by a show or hide.
     */
    toggleAccessibilityAttributes(show) {
        const expandedValue = (show === null || show === void 0 ? void 0 : show.toString()) || this.referenceElement.ariaExpanded || "false";
        this.referenceElement.ariaExpanded = expandedValue;
        this.referenceElement.setAttribute("aria-expanded", expandedValue);
    }
}
PopoverController.targets = [];
/**
 * Helper to manually show an s-popover element via external JS
 * @param element the element the `data-controller="s-popover"` attribute is on
 */
function showPopover(element) {
    const { isPopover, controller } = getPopover(element);
    if (controller) {
        controller.show();
    }
    else if (isPopover) {
        element.setAttribute("data-s-popover-auto-show", "true");
    }
    else {
        throw `element does not have data-controller="s-popover"`;
    }
}
/**
 * Helper to manually hide an s-popover element via external JS
 * @param element the element the `data-controller="s-popover"` attribute is on
 */
function hidePopover(element) {
    const { isPopover, controller, popover } = getPopover(element);
    if (controller) {
        controller.hide();
    }
    else if (isPopover) {
        element.removeAttribute("data-s-popover-auto-show");
        if (popover) {
            popover.classList.remove("is-visible");
        }
    }
    else {
        throw `element does not have data-controller="s-popover"`;
    }
}
/**
 * Attaches a popover to an element and performs additional configuration.
 * @param element the element that will receive the `data-controller="s-popover"` attribute.
 * @param popover an element with the `.s-popover` class or HTML string containing a single element with the `.s-popover` class.
 *                If the popover does not have a parent element, it will be inserted as a immediately after the reference element.
 * @param options an optional collection of options to use when configuring the popover.
 */
function attachPopover(element, popover, options) {
    const { referenceElement, popover: existingPopover } = getPopover(element);
    if (existingPopover) {
        throw `element already has popover with id="${existingPopover.id}"`;
    }
    if (!referenceElement) {
        throw `element has invalid data-s-popover-reference-selector attribute`;
    }
    if (typeof popover === "string") {
        // eslint-disable-next-line no-unsanitized/method
        const elements = document
            .createRange()
            .createContextualFragment(popover).children;
        if (elements.length !== 1) {
            throw "popover should contain a single element";
        }
        popover = elements[0];
    }
    const existingId = referenceElement.getAttribute("aria-controls");
    let popoverId = popover.id;
    if (!popover.classList.contains("s-popover")) {
        throw `popover should have the "s-popover" class but had class="${popover.className}"`;
    }
    if (existingId && existingId !== popoverId) {
        throw `element has aria-controls="${existingId}" but popover has id="${popoverId}"`;
    }
    if (!popoverId) {
        popoverId =
            "--stacks-s-popover-" + Math.random().toString(36).substring(2, 10);
        popover.id = popoverId;
    }
    if (!existingId) {
        referenceElement.setAttribute("aria-controls", popoverId);
    }
    if (!popover.parentElement && element.parentElement) {
        referenceElement.insertAdjacentElement("afterend", popover);
    }
    toggleController(element, "s-popover", true);
    if (options) {
        if (options.toggleOnClick) {
            referenceElement.setAttribute("data-action", "click->s-popover#toggle");
        }
        if (options.placement) {
            element.setAttribute("data-s-popover-placement", options.placement);
        }
        if (options.autoShow) {
            element.setAttribute("data-s-popover-auto-show", "true");
        }
    }
}
/**
 * Removes the popover controller from an element and removes the popover from the DOM.
 * @param element the element that has the `data-controller="s-popover"` attribute.
 * @returns The popover that was attached to the element.
 */
function detachPopover(element) {
    const { isPopover, controller, referenceElement, popover } = getPopover(element);
    // Hide the popover so its events fire.
    controller === null || controller === void 0 ? void 0 : controller.hide();
    // Remove the popover if it exists
    popover === null || popover === void 0 ? void 0 : popover.remove();
    // Remove the popover controller and the aria-controls attributes.
    if (isPopover) {
        toggleController(element, "s-popover", false);
        if (referenceElement) {
            referenceElement.removeAttribute("aria-controls");
        }
    }
    return popover;
}
/**
 * Gets the current state of an element that may be or is intended to be an s-popover controller
 * so it can be configured either directly or via the DOM.
 * @param element An element that may have `data-controller="s-popover"`.
 */
function getPopover(element) {
    var _a;
    const isPopover = ((_a = element.getAttribute("data-controller")) === null || _a === void 0 ? void 0 : _a.includes("s-popover")) || false;
    const controller = application.getControllerForElementAndIdentifier(element, "s-popover");
    const referenceSelector = element.getAttribute("data-s-popover-reference-selector");
    const referenceElement = referenceSelector
        ? element.querySelector(referenceSelector)
        : element;
    const popoverId = referenceElement
        ? referenceElement.getAttribute("aria-controls")
        : null;
    const popover = popoverId ? document.getElementById(popoverId) : null;
    return { isPopover, controller, referenceElement, popover };
}
/**
 * Adds or removes the controller from an element's [data-controller] attribute without altering existing entries
 * @param el The element to alter
 * @param controllerName The name of the controller to add/remove
 * @param include Whether to add the controllerName value
 */
function toggleController(el, controllerName, include) {
    var _a;
    const controllers = new Set((_a = el.getAttribute("data-controller")) === null || _a === void 0 ? void 0 : _a.split(/\s+/));
    if (include) {
        controllers.add(controllerName);
    }
    else {
        controllers.delete(controllerName);
    }
    el.setAttribute("data-controller", Array.from(controllers).join(" "));
}

;// CONCATENATED MODULE: ./lib/components/table/table.ts

/**
 * The string values of these enumerations should correspond with `aria-sort` valid values.
 *
 * @see https://developer.mozilla.org/en-US/docs/Web/Accessibility/ARIA/Attributes/aria-sort#values
 */
var SortOrder;
(function (SortOrder) {
    SortOrder["Ascending"] = "ascending";
    SortOrder["Descending"] = "descending";
    SortOrder["None"] = "none";
})(SortOrder || (SortOrder = {}));
class TableController extends StacksController {
    constructor() {
        super(...arguments);
        this.updateSortedColumnStyles = (targetColumnHeader, direction) => {
            // Loop through all sortable columns and remove their sorting direction
            // (if any), and only leave/set a sorting on `targetColumnHeader`.
            this.columnTargets.forEach((header) => {
                const isCurrent = header === targetColumnHeader;
                const classSuffix = isCurrent
                    ? direction === SortOrder.Ascending
                        ? "asc"
                        : "desc"
                    : SortOrder.None;
                header.classList.toggle("is-sorted", isCurrent && direction !== SortOrder.None);
                header.querySelectorAll(".js-sorting-indicator").forEach((icon) => {
                    icon.classList.toggle("d-none", !icon.classList.contains("js-sorting-indicator-" + classSuffix));
                });
                if (isCurrent) {
                    header.setAttribute("aria-sort", direction);
                }
                else {
                    header.removeAttribute("aria-sort");
                }
            });
        };
    }
    sort(evt) {
        // eslint-disable-next-line @typescript-eslint/no-this-alias
        const controller = this;
        const sortTriggerEl = evt.currentTarget;
        // TODO: support *only* button as trigger in next major release
        const triggerIsButton = sortTriggerEl instanceof HTMLButtonElement;
        // the below conditional is here for backward compatibility with the old API
        // where we did not advise buttons as sortable column head triggers
        const colHead = (triggerIsButton ? sortTriggerEl.parentElement : sortTriggerEl);
        const table = this.element;
        const tbody = table.tBodies[0];
        // the column slot number of the clicked header
        const colno = getCellSlot(colHead);
        if (colno < 0) {
            // this shouldn't happen if the clicked element is actually a column head
            return;
        }
        // an index of the <tbody>, so we can find out for each row which <td> element is
        // in the same column slot as the header
        const slotIndex = buildIndex(tbody);
        // the default behavior when clicking a header is to sort by this column in ascending
        // direction, *unless* it is already sorted that way
        const direction = colHead.getAttribute("aria-sort") === SortOrder.Ascending ? -1 : 1;
        const rows = Array.from(table.tBodies[0].rows);
        // if this is still false after traversing the data, that means all values are integers (or empty)
        // and thus we'll sort numerically.
        let anyNonInt = false;
        // data will be a list of tuples [value, rowNum], where value is what we're sorting by
        const data = [];
        let firstBottomRow;
        rows.forEach(function (row, index) {
            var _a, _b;
            const force = controller.getElementData(row, "sort-to");
            if (force === "top") {
                return; // rows not added to the list will automatically end up at the top
            }
            else if (force === "bottom") {
                if (!firstBottomRow) {
                    firstBottomRow = row;
                }
                return;
            }
            const cell = slotIndex[index][colno];
            if (!cell) {
                data.push(["", index]);
                return;
            }
            // unless the to-be-sorted-by value is explicitly provided on the element via this attribute,
            // the value we're using is the cell's text, trimmed of any whitespace
            const explicit = controller.getElementData(cell, "sort-val");
            const d = (_b = explicit !== null && explicit !== void 0 ? explicit : (_a = cell.textContent) === null || _a === void 0 ? void 0 : _a.trim()) !== null && _b !== void 0 ? _b : "";
            if (d !== "" && `${parseInt(d, 10)}` !== d) {
                anyNonInt = true;
            }
            data.push([d, index]);
        });
        // If all values were integers (or empty cells), sort numerically, with empty cells treated as
        // having the lowest possible value (i.e. sorted to the top if ascending, bottom if descending)
        if (!anyNonInt) {
            data.forEach(function (tuple) {
                tuple[0] =
                    tuple[0] === ""
                        ? Number.MIN_VALUE
                        : parseInt(tuple[0], 10);
            });
        }
        // We don't sort an array of <tr>, but instead an arrays of row *numbers*, because this way we
        // can enforce stable sorting, i.e. rows that compare equal are guaranteed to remain in the same
        // order (the JS standard does not gurantee this for sort()).
        data.sort(function (a, b) {
            // first compare the values (a[0])
            if (a[0] > b[0]) {
                return 1 * direction;
            }
            else if (a[0] < b[0]) {
                return -1 * direction;
            }
            else {
                // if the values are equal, compare the row numbers (a[1]) to guarantee stable sorting
                // (note that this comparison is independent of the sorting direction)
                return a[1] > b[1] ? 1 : -1;
            }
        });
        // this is the actual reordering of the table rows
        data.forEach(([_, rowIndex]) => {
            var _a;
            const row = rows[rowIndex];
            (_a = row.parentElement) === null || _a === void 0 ? void 0 : _a.removeChild(row);
            if (firstBottomRow) {
                tbody.insertBefore(row, firstBottomRow);
            }
            else {
                tbody.appendChild(row);
            }
        });
        // update the UI and set the `data-sort-direction` attribute if appropriate, so that the next click
        // will cause sorting in descending direction
        this.updateSortedColumnStyles(colHead, direction === 1 ? SortOrder.Ascending : SortOrder.Descending);
    }
}
TableController.targets = ["column"];
/**
 * @internal This function is exported for testing purposes but is not a part of our public API
 *
 * @param section
 */
function buildIndex(section) {
    const result = buildIndexOrGetCellSlot(section);
    if (!Array.isArray(result)) {
        throw "shouldn't happen";
    }
    return result;
}
/**
 * @internal This function is exported for testing purposes but is not a part of our public API
 *
 * @param cell
 */
function getCellSlot(cell) {
    var _a;
    const tableElement = (_a = cell.parentElement) === null || _a === void 0 ? void 0 : _a.parentElement;
    if (!(tableElement instanceof HTMLTableSectionElement)) {
        throw "invalid table";
    }
    const result = buildIndexOrGetCellSlot(tableElement, cell);
    if (typeof result !== "number") {
        throw "shouldn't happen";
    }
    return result;
}
/**
 * Just because a <td> is the 4th *child* of its <tr> doesn't mean it belongs to the 4th *column*
 * of the table. Previous cells may have a colspan; cells in previous rows may have a rowspan.
 * Because we need to know which header cells and data cells belong together, we have to 1) find out
 * which column number (or "slot" as we call it here) the header cell has, and 2) for each row find
 * out which <td> cell corresponds to this slot (because those are the rows we're sorting by).
 *
 * That's what the following function does. If the second argument is not given, it returns an index
 * of the table, which is an array of arrays. Each of the sub-arrays corresponds to a table row. The
 * indices of the sub-array correspond to column slots; the values are the actual table cell elements.
 * For example index[4][3] is the <td> or <th> in row 4, column 3 of the table section (<tbody> or <thead>).
 * Note that this element is not necessarily even in the 4th (zero-based) <tr> -- if it has a rowSpan > 1,
 * it may also be in a previous <tr>.
 *
 * If the second argument is given, it's a <td> or <th> that we're trying to find, and the algorithm
 * stops as soon as it has found it and the function returns its slot number.
 */
function buildIndexOrGetCellSlot(section, findCell) {
    const index = [];
    let curRow = section.children[0];
    // the elements of these two arrays are synchronized; the first array contains table cell elements,
    // the second one contains a number that indicates for how many more rows this elements will
    // exist (i.e. the value is initially one less than the cell's rowspan, and will be decreased for each row)
    const growing = [];
    const growingRowsLeft = [];
    // continue while we have actual <tr>'s left *or* we still have rowspan'ed elements that aren't done
    while (curRow || growingRowsLeft.some((e) => e !== 0)) {
        const curIndexRow = [];
        index.push(curIndexRow);
        let curSlot = 0;
        if (curRow) {
            for (let curCellIdx = 0; curCellIdx < curRow.children.length; curCellIdx++) {
                while (growingRowsLeft[curSlot]) {
                    growingRowsLeft[curSlot]--;
                    curIndexRow[curSlot] = growing[curSlot];
                    curSlot++;
                }
                const cell = curRow.children[curCellIdx];
                if (!(cell instanceof HTMLTableCellElement)) {
                    throw "invalid table";
                }
                if (getComputedStyle(cell).display === "none") {
                    continue;
                }
                if (cell === findCell) {
                    return curSlot;
                }
                const nextFreeSlot = curSlot + cell.colSpan;
                for (; curSlot < nextFreeSlot; curSlot++) {
                    growingRowsLeft[curSlot] = cell.rowSpan - 1; // if any of these is already growing, the table is broken -- no guarantees of anything
                    growing[curSlot] = cell;
                    curIndexRow[curSlot] = cell;
                }
            }
        }
        while (curSlot < growing.length) {
            if (growingRowsLeft[curSlot]) {
                growingRowsLeft[curSlot]--;
                curIndexRow[curSlot] = growing[curSlot];
            }
            curSlot++;
        }
        if (curRow) {
            curRow = curRow.nextElementSibling;
        }
    }
    // if findCell was given, but we end up here, that means it isn't in this section
    return findCell ? -1 : index;
}

;// CONCATENATED MODULE: ./lib/components/toast/toast.ts

class ToastController extends StacksController {
    connect() {
        this.validate();
    }
    /**
     * Disconnects all added event listeners on controller disconnect
     */
    disconnect() {
        this.unbindDocumentEvents();
    }
    /**
     * Toggles the visibility of the toast
     */
    toggle(dispatcher = null) {
        this._toggle(undefined, dispatcher);
    }
    /**
     * Shows the toast
     */
    show(dispatcher = null) {
        this._toggle(true, dispatcher);
    }
    /**
     * Hides the toast
     */
    hide(dispatcher = null) {
        this._toggle(false, dispatcher);
    }
    /**
     * Validates the toast settings and attempts to set necessary internal variables
     */
    validate() {
        // check for returnElement support
        const returnElementSelector = this.data.get("return-element");
        if (returnElementSelector) {
            this.returnElement = (document.querySelector(returnElementSelector));
            if (!this.returnElement) {
                throw ("Unable to find element by return-element selector: " +
                    returnElementSelector);
            }
        }
    }
    /**
     * Toggles the visibility of the toast element
     * @param show Optional parameter that force shows/hides the element or toggles it if left undefined
     */
    _toggle(show, dispatcher = null) {
        let toShow = show;
        const isVisible = this.toastTarget.getAttribute("aria-hidden") === "false";
        // if we're letting the class toggle, we need to figure out if the toast is visible manually
        if (typeof toShow === "undefined") {
            toShow = !isVisible;
        }
        // if the state matches the disired state, return without changing anything
        if ((toShow && isVisible) || (!toShow && !isVisible)) {
            return;
        }
        const dispatchingElement = this.getDispatcher(dispatcher);
        // show/hide events trigger before toggling the class
        const triggeredEvent = this.triggerEvent(toShow ? "show" : "hide", {
            returnElement: this.returnElement,
            dispatcher: this.getDispatcher(dispatchingElement),
        }, this.toastTarget);
        // if this pre-show/hide event was prevented, don't attempt to continue changing the toast state
        if (triggeredEvent.defaultPrevented) {
            return;
        }
        this.returnElement = triggeredEvent.detail.returnElement;
        this.toastTarget.setAttribute("aria-hidden", toShow ? "false" : "true");
        if (toShow) {
            this.bindDocumentEvents();
            this.hideAfterTimeout();
            if (this.data.get("prevent-focus-capture") !== "true") {
                this.focusInsideToast();
            }
        }
        else {
            this.unbindDocumentEvents();
            this.focusReturnElement();
            this.removeToastOnHide();
            this.clearActiveTimeout();
        }
        // check for transitionend support
        const supportsTransitionEnd = this.toastTarget.ontransitionend !== undefined;
        // shown/hidden events trigger after toggling the class
        if (supportsTransitionEnd) {
            // wait until after the toast finishes transitioning to fire the event
            this.toastTarget.addEventListener("transitionend", () => {
                //TODO this is firing waaay to soon?
                this.triggerEvent(toShow ? "shown" : "hidden", {
                    dispatcher: dispatchingElement,
                }, this.toastTarget);
            }, { once: true });
        }
        else {
            this.triggerEvent(toShow ? "shown" : "hidden", {
                dispatcher: dispatchingElement,
            }, this.toastTarget);
        }
    }
    /**
     * Listens for the s-toast:hidden event and focuses the returnElement when it is fired
     */
    focusReturnElement() {
        if (!this.returnElement) {
            return;
        }
        this.toastTarget.addEventListener("s-toast:hidden", () => {
            // double check the element still exists when the event is called
            if (this.returnElement &&
                document.body.contains(this.returnElement)) {
                this.returnElement.focus();
            }
        }, { once: true });
    }
    /**
     * Remove the element on hide if the `remove-when-hidden` flag is set
     */
    removeToastOnHide() {
        if (this.data.get("remove-when-hidden") !== "true") {
            return;
        }
        this.toastTarget.addEventListener("s-toast:hidden", () => {
            this.element.remove();
        }, { once: true });
    }
    /**
     * Hide the element after a delay
     */
    hideAfterTimeout() {
        if (this.data.get("prevent-auto-hide") === "true" ||
            this.data.get("hide-after-timeout") === "0") {
            return;
        }
        const timeout = parseInt(this.data.get("hide-after-timeout"), 10) || 3000;
        this.activeTimeout = window.setTimeout(() => this.hide(), timeout);
    }
    /**
     * Cancels the activeTimeout
     */
    clearActiveTimeout() {
        clearTimeout(this.activeTimeout);
    }
    /**
     * Gets all elements within the toast that could receive keyboard focus.
     */
    getAllTabbables() {
        return Array.from(this.toastTarget.querySelectorAll("[href], input, select, textarea, button, [tabindex]")).filter((el) => el.matches(":not([disabled]):not([tabindex='-1'])"));
    }
    /**
     * Returns the first visible element in an array or `undefined` if no elements are visible.
     */
    firstVisible(elements) {
        // https://stackoverflow.com/a/21696585
        return elements === null || elements === void 0 ? void 0 : elements.find((el) => el.offsetParent !== null);
    }
    /**
     * Attempts to shift keyboard focus into the toast.
     * If elements with `data-s-toast-target="initialFocus"` are present and visible, one of those will be selected.
     * Otherwise, the first visible focusable element will receive focus.
     */
    focusInsideToast() {
        this.toastTarget.addEventListener("s-toast:shown", () => {
            var _a;
            const initialFocus = (_a = this.firstVisible(this.initialFocusTargets)) !== null && _a !== void 0 ? _a : this.firstVisible(this.getAllTabbables());
            initialFocus === null || initialFocus === void 0 ? void 0 : initialFocus.focus();
        }, { once: true });
    }
    /**
     * Binds global events to the document for hiding toasts on user interaction
     */
    bindDocumentEvents() {
        // in order for removeEventListener to remove the right event, this bound function needs a constant reference
        this._boundClickFn =
            this._boundClickFn || this.hideOnOutsideClick.bind(this);
        this._boundKeypressFn =
            this._boundKeypressFn || this.hideOnEscapePress.bind(this);
        document.addEventListener("mousedown", this._boundClickFn);
        document.addEventListener("keyup", this._boundKeypressFn);
    }
    /**
     * Unbinds global events to the document for hiding toasts on user interaction
     */
    unbindDocumentEvents() {
        document.removeEventListener("mousedown", this._boundClickFn);
        document.removeEventListener("keyup", this._boundKeypressFn);
    }
    /**
     * Forces the toast to hide if a user clicks outside of it or its reference element
     */
    hideOnOutsideClick(e) {
        var _a;
        const target = e.target;
        // check if the document was clicked inside either the toggle element or the toast itself
        // note: .contains also returns true if the node itself matches the target element
        if (!((_a = this.toastTarget) === null || _a === void 0 ? void 0 : _a.contains(target)) &&
            document.body.contains(target) &&
            this.data.get("hide-on-outside-click") !== "false") {
            this._toggle(false, e);
        }
    }
    /**
     * Forces the toast to hide if the user presses escape while it, one of its childen, or the reference element are focused
     */
    hideOnEscapePress(e) {
        // if the ESC key (27) wasn't pressed or if no toasts are showing, return
        if (e.which !== 27 ||
            this.toastTarget.getAttribute("aria-hidden") === "true") {
            return;
        }
        this._toggle(false, e);
    }
    /**
     * Determines the correct dispatching element from a potential input
     * @param dispatcher The event or element to get the dispatcher from
     */
    getDispatcher(dispatcher = null) {
        if (dispatcher instanceof Event) {
            return dispatcher.target;
        }
        else if (dispatcher instanceof Element) {
            return dispatcher;
        }
        else {
            return this.element;
        }
    }
}
ToastController.targets = ["toast", "initialFocus"];
/**
 * Helper to manually show an s-toast element via external JS
 * @param element the element the `data-controller="s-toast"` attribute is on
 */
function showToast(element) {
    toggleToast(element, true);
}
/**
 * Helper to manually hide an s-toast element via external JS
 * @param element the element the `data-controller="s-toast"` attribute is on
 */
function hideToast(element) {
    toggleToast(element, false);
}
/**
 * Helper to manually show an s-toast element via external JS
 * @param element the element the `data-controller="s-toast"` attribute is on
 * @param show whether to force show/hide the toast; toggles the toast if left undefined
 */
function toggleToast(element, show) {
    const controller = application.getControllerForElementAndIdentifier(element, "s-toast");
    if (!controller) {
        throw "Unable to get s-toast controller from element";
    }
    show ? controller.show() : controller.hide();
}

;// CONCATENATED MODULE: ./lib/components/popover/tooltip.ts


class TooltipController extends BasePopoverController {
    constructor() {
        super(...arguments);
        this.popoverSelectorAttribute = "aria-describedby";
    }
    /**
     * Binds mouseover and mouseout events in addition to BasePopoverController.connect
     */
    connect() {
        super.connect();
        // Only bind to mouse events if the pointer device supports hover behavior.
        // Otherwise we run into issues with mobile browser showing popovers for
        // click events and not being able to hide them.
        if (window.matchMedia("(hover: hover)").matches) {
            this.bindMouseEvents();
        }
        this.bindKeyboardEvents();
    }
    /**
     * Unbinds mouse events in addition to BasePopoverController.disconnect
     */
    disconnect() {
        this.unbindKeyboardEvents();
        this.unbindMouseEvents();
        super.disconnect();
    }
    /**
     * Attempts to show the tooltip popover so long as no other Stacks-managed popover is
     * present on the page.
     */
    show(dispatcher = null) {
        // check and see if this controller coexists with a popover
        const controller = application.getControllerForElementAndIdentifier(this.element, "s-popover");
        // if the controller exists and already has a visible popover, don't show the tooltip
        if (controller && controller.isVisible) {
            return;
        }
        super.show(dispatcher);
    }
    /**
     * Sets up a tooltip popover show after a delay.
     */
    scheduleShow(dispatcher = null) {
        window.clearTimeout(this.activeTimeout);
        this.activeTimeout = window.setTimeout(() => this.show(dispatcher), 300);
    }
    /**
     * Cancels the scheduled tooltip popover display and hides it if already displayed
     */
    scheduleHide(dispatcher = null) {
        window.clearTimeout(this.activeTimeout);
        this.activeTimeout = window.setTimeout(() => super.hide(dispatcher), 100);
    }
    /**
     * Cancels the activeTimeout
     */
    clearActiveTimeout() {
        clearTimeout(this.activeTimeout);
    }
    /**
     * Applies data-s-tooltip-html-title and title attributes.
     */
    applyTitleAttributes() {
        let content;
        const htmlTitle = this.data.get("html-title");
        if (htmlTitle) {
            // eslint-disable-next-line no-unsanitized/method
            content = document
                .createRange()
                .createContextualFragment(htmlTitle);
        }
        else {
            const plainTitle = this.element.getAttribute("title");
            if (plainTitle) {
                content = document.createTextNode(plainTitle);
            }
            else {
                return null;
            }
        }
        this.data.delete("html-title");
        this.element.removeAttribute("title");
        let popoverId = this.element.getAttribute("aria-describedby");
        if (!popoverId) {
            popoverId = TooltipController.generateId();
            this.element.setAttribute("aria-describedby", popoverId);
        }
        let popover = document.getElementById(popoverId);
        if (!popover) {
            popover = document.createElement("div");
            popover.id = popoverId;
            popover.className = "s-popover s-popover__tooltip";
            popover.setAttribute("role", "tooltip");
            const parentNode = this.element.parentNode;
            if (parentNode) {
                // insertBefore inserts at end if element.nextSibling is null.
                parentNode.insertBefore(popover, this.element.nextSibling);
            }
            else {
                document.body.appendChild(popover);
            }
        }
        const arrow = popover.querySelector(".s-popover--arrow");
        // clear and set the content of the popover
        popover.innerHTML = "";
        popover.appendChild(content);
        // create the arrow if necessary
        if (arrow) {
            popover.appendChild(arrow);
        }
        else {
            popover.insertAdjacentHTML("beforeend", `<div class="s-popover--arrow"></div>`);
        }
        this.scheduleUpdate();
        return popover;
    }
    /**
     * Automatically hides the tooltip popover when a Stacks popover is shown anywhere on
     * the page.
     */
    bindDocumentEvents() {
        this.boundHideIfWithin =
            this.boundHideIfWithin || this.hideIfWithin.bind(this);
        document.addEventListener("s-popover:shown", this.boundHideIfWithin);
    }
    /**
     * Unbinds all mouse events
     */
    unbindDocumentEvents() {
        document.removeEventListener("s-popover:shown", this.boundHideIfWithin);
    }
    /**
     * Attempts to generate a new tooltip popover from the title attribute if no popover
     * was present when requested, otherwise throws an error.
     */
    generatePopover() {
        return this.applyTitleAttributes();
    }
    /**
     * Hides the tooltip if is or is within the event's target.
     * @param event An event object from s-popover:shown
     */
    hideIfWithin(event) {
        if (event.target.contains(this.referenceElement)) {
            this.scheduleHide();
        }
    }
    hideOnEscapeKeyEvent(event) {
        if (event.key === "Escape") {
            this.scheduleHide();
        }
    }
    /**
     * Binds mouse events to show/hide on reference element hover
     */
    bindKeyboardEvents() {
        this.boundScheduleShow =
            this.boundScheduleShow || this.scheduleShow.bind(this);
        this.boundHide = this.boundHide || this.scheduleHide.bind(this);
        this.boundHideOnEscapeKeyEvent =
            this.boundHideOnEscapeKeyEvent ||
                this.hideOnEscapeKeyEvent.bind(this);
        this.referenceElement.addEventListener("focus", this.boundScheduleShow);
        this.referenceElement.addEventListener("blur", this.boundHide);
        document.addEventListener("keyup", this.boundHideOnEscapeKeyEvent);
    }
    /**
     * Unbinds all mouse events
     */
    unbindKeyboardEvents() {
        this.referenceElement.removeEventListener("focus", this.boundScheduleShow);
        this.referenceElement.removeEventListener("blur", this.boundHide);
        document.removeEventListener("keyup", this.boundHideOnEscapeKeyEvent);
    }
    /**
     * Binds mouse events to show/hide on reference element hover
     */
    bindMouseEvents() {
        this.boundScheduleShow =
            this.boundScheduleShow || this.scheduleShow.bind(this);
        this.boundHide = this.boundHide || this.scheduleHide.bind(this);
        this.boundClearActiveTimeout =
            this.boundClearActiveTimeout || this.clearActiveTimeout.bind(this);
        this.referenceElement.addEventListener("mouseover", this.boundScheduleShow);
        this.referenceElement.addEventListener("mouseout", this.boundHide);
        this.popoverElement.addEventListener("mouseover", this.boundClearActiveTimeout);
        this.popoverElement.addEventListener("mouseout", this.boundHide);
    }
    /**
     * Unbinds all mouse events
     */
    unbindMouseEvents() {
        this.referenceElement.removeEventListener("mouseover", this.boundScheduleShow);
        this.referenceElement.removeEventListener("mouseout", this.boundHide);
        this.referenceElement.removeEventListener("focus", this.boundScheduleShow);
        this.referenceElement.removeEventListener("blur", this.boundHide);
        this.popoverElement.removeEventListener("mouseover", this.boundClearActiveTimeout);
        this.popoverElement.removeEventListener("mouseout", this.boundHide);
    }
    /**
     * Generates an ID for tooltips created with setTooltip.
     */
    static generateId() {
        // generate a random number, then convert to a well formatted string
        return ("--stacks-s-tooltip-" + Math.random().toString(36).substring(2, 10));
    }
}
TooltipController.targets = [];
/**
 * Adds or updates a Stacks tooltip on a given element, initializing the controller if necessary
 * @param element The element to add a tooltip to.
 * @param html An HTML string to populate the tooltip with.
 * @param options Options for rendering the tooltip.
 */
function setTooltipHtml(element, html, options) {
    element.setAttribute("data-s-tooltip-html-title", html);
    element.removeAttribute("title");
    applyOptionsAndTitleAttributes(element, options);
}
/**
 * Adds or updates a Stacks tooltip on a given element, initializing the controller if necessary
 * @param element The element to add a tooltip to.
 * @param text A plain text string to populate the tooltip with.
 * @param options Options for rendering the tooltip.
 */
function setTooltipText(element, text, options) {
    element.setAttribute("title", text);
    element.removeAttribute("data-s-tooltip-html-title");
    applyOptionsAndTitleAttributes(element, options);
}
/**
 * Shared helper for setTooltip* to initialize and set tooltip content
 * @param element The element to add a tooltip to.
 * @param options Options for rendering the tooltip.
 */
function applyOptionsAndTitleAttributes(element, options) {
    if (options && options.placement) {
        element.setAttribute("data-s-tooltip-placement", options.placement);
    }
    const controller = (application.getControllerForElementAndIdentifier(element, "s-tooltip"));
    if (controller) {
        controller.applyTitleAttributes();
    }
    else {
        const dataController = element.getAttribute("data-controller");
        element.setAttribute("data-controller", `${dataController ? dataController : ""} s-tooltip`);
    }
}

;// CONCATENATED MODULE: ./lib/components/uploader/uploader.ts

class UploaderController extends StacksController {
    connect() {
        super.connect();
        this.boundDragEnter = this.handleUploaderActive.bind(this, true);
        this.boundDragLeave = this.handleUploaderActive.bind(this, false);
        this.inputTarget.addEventListener("dragenter", this.boundDragEnter);
        this.inputTarget.addEventListener("dragleave", this.boundDragLeave);
    }
    disconnect() {
        this.inputTarget.removeEventListener("dragenter", this.boundDragEnter);
        this.inputTarget.removeEventListener("dragleave", this.boundDragLeave);
        super.disconnect();
    }
    /**
     * Handles rendering the file preview state on input change
     */
    handleInput() {
        this.previewsTarget.innerHTML = "";
        if (!this.inputTarget.files) {
            return;
        }
        const count = this.inputTarget.files.length;
        this.getDataURLs(this.inputTarget.files, UploaderController.FILE_DISPLAY_LIMIT)
            .then((res) => {
            this.handleVisible(true);
            const hasMultipleFiles = res.length > 1;
            if (hasMultipleFiles) {
                const headingElement = document.createElement("div");
                headingElement.classList.add("s-uploader--previews-heading");
                headingElement.innerText =
                    res.length < count
                        ? `Showing ${res.length} of ${count} files`
                        : `${count} items`;
                this.previewsTarget.appendChild(headingElement);
                this.previewsTarget.classList.add("has-multiple");
            }
            else {
                this.previewsTarget.classList.remove("has-multiple");
            }
            res.forEach((file) => this.addFilePreview(file));
            this.handleUploaderActive(true);
        })
            // TODO consider rendering an error message
            .catch(() => null);
    }
    /**
     * Resets the Uploader to initial state
     */
    reset() {
        this.inputTarget.value = "";
        this.previewsTarget.innerHTML = "";
        this.handleVisible(false);
    }
    /**
     * Set hide/show and disabled state on elements depending on preview state
     * @param  {boolean} shouldPreview - Uploader is entering a preview state
     */
    handleVisible(shouldPreview) {
        const { scope } = this.targets;
        const hideElements = scope.findAllElements("[data-s-uploader-hide-on-input]");
        const showElements = scope.findAllElements("[data-s-uploader-show-on-input]");
        const enableElements = scope.findAllElements("[data-s-uploader-enable-on-input]");
        if (shouldPreview) {
            hideElements.forEach((el) => {
                el.classList.add("d-none");
            });
            showElements.forEach((el) => {
                el.classList.remove("d-none");
            });
            enableElements.forEach((el) => {
                el.removeAttribute("disabled");
            });
        }
        else {
            hideElements.forEach((el) => {
                el.classList.remove("d-none");
            });
            showElements.forEach((el) => {
                el.classList.add("d-none");
            });
            enableElements.forEach((el) => {
                el.setAttribute("disabled", "true");
            });
            this.handleUploaderActive(false);
        }
    }
    /**
     * Adds a DOM element to preview a selected file
     * @param  {FilePreview} file
     */
    addFilePreview(file) {
        if (!file) {
            return;
        }
        const previewElement = document.createElement("div");
        let thumbElement;
        if (file.type.match("image/*") && file.data) {
            thumbElement = document.createElement("img");
            // eslint-disable-next-line @typescript-eslint/no-base-to-string
            thumbElement.src = file.data.toString();
            thumbElement.alt = file.name;
        }
        else {
            thumbElement = document.createElement("div");
            thumbElement.innerText = file.name;
        }
        thumbElement.classList.add("s-uploader--preview-thumbnail");
        previewElement.appendChild(thumbElement);
        previewElement.classList.add("s-uploader--preview");
        previewElement.setAttribute("data-filename", file.name);
        this.previewsTarget.appendChild(previewElement);
    }
    /**
     * Toggles display and disabled state for select elements on valid input
     * @param  {boolean} active - Uploader is in active state (typically on 'dragenter')
     */
    handleUploaderActive(active) {
        this.uploaderTarget.classList.toggle("is-active", active);
    }
    /**
     * Converts the file data into a data URL
     * @param  {File} file
     * @returns an object containing a FilePreview object
     */
    fileToDataURL(file) {
        const reader = new FileReader();
        const { name, size, type } = file;
        if (size < UploaderController.MAX_FILE_SIZE &&
            type.indexOf("image") > -1) {
            return new Promise((resolve, reject) => {
                reader.onload = (evt) => {
                    var _a;
                    const res = (_a = evt === null || evt === void 0 ? void 0 : evt.target) === null || _a === void 0 ? void 0 : _a.result;
                    if (res) {
                        resolve({ data: res, name, type });
                    }
                    else {
                        reject();
                    }
                };
                reader.readAsDataURL(file);
            });
        }
        else {
            return Promise.resolve({ name, type });
        }
    }
    /**
     * Gets an array of FilePreviews from a FileList
     * @param  {FileList|[]} files
     * @returns an array of FilePreview objects from a FileList
     */
    getDataURLs(files, limit) {
        const promises = Array.from(files)
            .slice(0, Math.min(limit, files.length))
            .map((f) => this.fileToDataURL(f));
        return Promise.all(promises);
    }
}
UploaderController.targets = ["input", "previews", "uploader"];
UploaderController.FILE_DISPLAY_LIMIT = 10;
UploaderController.MAX_FILE_SIZE = 1024 * 1024 * 10; // 10 MB

;// CONCATENATED MODULE: ./lib/controllers.ts
// export all controllers *with helpers* so they can be bulk re-exported by the package entry point










;// CONCATENATED MODULE: ./lib/index.ts



// register all built-in controllers
application.register("s-banner", BannerController);
application.register("s-expandable-control", ExpandableController);
application.register("s-modal", ModalController);
application.register("s-toast", ToastController);
application.register("s-navigation-tablist", TabListController);
application.register("s-popover", PopoverController);
application.register("s-table", TableController);
application.register("s-tooltip", TooltipController);
application.register("s-uploader", UploaderController);
// finalize the application to guard our controller namespace
StacksApplication.finalize();
// export all controllers w/ helpers

// export the entirety of the contents of stacks.ts


/******/ 	return __webpack_exports__;
/******/ })()
;
});