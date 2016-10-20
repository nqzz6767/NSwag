//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v3.10.6019.184 (http://NSwag.org)
// </auto-generated>
//----------------------
define(["require", "exports"], function (require, exports) {
    "use strict";
    var DataService = (function () {
        function DataService(baseUrl) {
            this.baseUrl = undefined;
            this.beforeSend = undefined;
            this.baseUrl = baseUrl !== undefined ? baseUrl : "";
        }
        DataService.prototype.xyz = function (data, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Person/xyz/{data}?";
            if (data === undefined || data === null)
                throw new Error("The parameter 'data' must be defined.");
            url = url.replace("{data}", encodeURIComponent("" + data));
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "put",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processXyzWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processXyzWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processXyzWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processXyz(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processXyz = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        /**
         * @deprecated
         */
        DataService.prototype.get = function (onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/Get?";
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processGetWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processGetWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processGetWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processGet(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processGet = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        /**
         * Gets a person.
         * @id The ID of
    the person.
         * @return The person.
         */
        DataService.prototype.get2 = function (id, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/Get/{id}?";
            if (id === undefined || id === null)
                throw new Error("The parameter 'id' must be defined.");
            url = url.replace("{id}", encodeURIComponent("" + id));
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processGet2WithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processGet2WithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processGet2WithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processGet2(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processGet2 = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else if (status === "500") {
                var result500 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result500 = data === "" ? null : jQuery.parseJSON(data);
                }
                throw result500;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        /**
         * Creates a new person.
         * @value (optional) The person.
         */
        DataService.prototype.post = function (value, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/Post?";
            var content = JSON.stringify(value);
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "post",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processPostWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processPostWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processPostWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processPost(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processPost = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "204") {
                var result204 = undefined;
                return result204;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        /**
         * Updates the existing person.
         * @id The ID.
         * @value (optional) The person.
         */
        DataService.prototype.put = function (id, value, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/Put/{id}?";
            if (id === undefined || id === null)
                throw new Error("The parameter 'id' must be defined.");
            url = url.replace("{id}", encodeURIComponent("" + id));
            var content = JSON.stringify(value);
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "put",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processPutWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processPutWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processPutWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processPut(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processPut = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "204") {
                var result204 = undefined;
                return result204;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        DataService.prototype.delete = function (id, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/Delete/{id}?";
            if (id === undefined || id === null)
                throw new Error("The parameter 'id' must be defined.");
            url = url.replace("{id}", encodeURIComponent("" + id));
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "delete",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processDeleteWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processDeleteWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processDeleteWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processDelete(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processDelete = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "204") {
                var result204 = undefined;
                return result204;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        /**
         * Calculates the sum of a, b and c.
         */
        DataService.prototype.calculate = function (a, b, c, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Person/Calculate/{a}/{b}?";
            if (a === undefined || a === null)
                throw new Error("The parameter 'a' must be defined.");
            url = url.replace("{a}", encodeURIComponent("" + a));
            if (b === undefined || b === null)
                throw new Error("The parameter 'b' must be defined.");
            url = url.replace("{b}", encodeURIComponent("" + b));
            if (c === undefined || c === null)
                throw new Error("The parameter 'c' must be defined.");
            else
                url += "c=" + encodeURIComponent("" + c) + "&";
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processCalculateWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processCalculateWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processCalculateWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processCalculate(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processCalculate = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        DataService.prototype.addHour = function (time, onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/AddHour?";
            if (time === undefined || time === null)
                throw new Error("The parameter 'time' must be defined.");
            else
                url += "time=" + encodeURIComponent("" + time.toJSON()) + "&";
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processAddHourWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processAddHourWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processAddHourWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processAddHour(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processAddHour = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = new Date(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        DataService.prototype.test = function (onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/TestAsync?";
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processTestWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processTestWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processTestWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processTest(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processTest = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        DataService.prototype.loadComplexObject = function (onSuccess, onFail) {
            var _this = this;
            var url = this.baseUrl + "/api/Persons/LoadComplexObject2?";
            var content = "";
            jQuery.ajax({
                url: url,
                beforeSend: this.beforeSend,
                type: "get",
                data: content,
                dataType: "text",
                headers: {
                    "Content-Type": "application/json; charset=UTF-8"
                }
            }).done(function (data, textStatus, xhr) {
                _this.processLoadComplexObjectWithCallbacks(url, xhr, onSuccess, onFail);
            }).fail(function (xhr) {
                _this.processLoadComplexObjectWithCallbacks(url, xhr, onSuccess, onFail);
            });
        };
        DataService.prototype.processLoadComplexObjectWithCallbacks = function (url, xhr, onSuccess, onFail) {
            try {
                var result = this.processLoadComplexObject(xhr);
                if (onSuccess !== undefined)
                    onSuccess(result);
            }
            catch (e) {
                if (onFail !== undefined)
                    onFail(e, "http_service_exception");
            }
        };
        DataService.prototype.processLoadComplexObject = function (xhr) {
            var data = xhr.responseText;
            var status = xhr.status.toString();
            if (status === "200") {
                var result200 = null;
                if (data !== undefined && data !== null && data !== "") {
                    result200 = data === "" ? null : jQuery.parseJSON(data);
                }
                return result200;
            }
            else {
                throw new Error("error_no_callback_for_the_received_http_status");
            }
        };
        return DataService;
    }());
    exports.DataService = DataService;
    /** Foo bar */
    (function (ObjectType) {
        ObjectType[ObjectType["Foo"] = "Foo"] = "Foo";
        ObjectType[ObjectType["Bar"] = "Bar"] = "Bar";
    })(exports.ObjectType || (exports.ObjectType = {}));
    var ObjectType = exports.ObjectType;
});
//# sourceMappingURL=DataService.js.map