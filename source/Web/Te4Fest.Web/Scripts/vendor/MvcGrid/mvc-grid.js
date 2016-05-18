﻿/*!
 * Mvc.Grid 2.3.2
 * https://github.com/NonFactors/MVC5.Grid
 *
 * Copyright © NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */
var MvcGrid = (function () {
    function MvcGrid(grid, options) {
        this.columns = [];
        this.element = grid;
        options = options || {};
        this.name = grid.data('name') || '';
        this.rowClicked = options.rowClicked;
        this.reloadEnded = options.reloadEnded;
        this.reloadFailed = options.reloadFailed;
        this.reloadStarted = options.reloadStarted;
        this.sourceUrl = options.sourceUrl || grid.data('source-url') || '';
        this.filters = $.extend({
            'Text': new MvcGridTextFilter(),
            'Date': new MvcGridDateFilter(),
            'Number': new MvcGridNumberFilter(),
            'Boolean': new MvcGridBooleanFilter()
        }, options.filters);

        if (this.sourceUrl != '') {
            var splitIndex = this.sourceUrl.indexOf('?');
            if (splitIndex > -1) {
                this.gridQuery = this.sourceUrl.substring(splitIndex + 1);
                this.sourceUrl = this.sourceUrl.substring(0, splitIndex);
            } else {
                this.gridQuery = options.query || '';
            }
        } else {
            this.gridQuery = window.location.search.replace('?', '');
        }

        if (options.reload === true || (this.sourceUrl != '' && !options.isLoaded)) {
            this.reload(this.gridQuery);
            return;
        }

        var headers = grid.find('.mvc-grid-header');
        for (var i = 0; i < headers.length; i++) {
            var column = this.createColumn($(headers[i]));
            this.applyFiltering(column);
            this.applySorting(column);
            this.columns.push(column);
            this.cleanHeader(column);
        }

        var pages = grid.find('.mvc-grid-pager span');
        for (var ind = 0; ind < pages.length; ind++) {
            this.applyPaging($(pages[ind]));
        }

        this.bindGridEvents();
        this.cleanGrid(grid);
    }

    MvcGrid.prototype = {
        createColumn: function (header) {
            return {
                name: header.data('name') || '',
                header: header,
                filter: {
                    isEnabled: header.data('filterable') == 'True',
                    isMulti: header.data('filter-multi') == 'True',
                    operator: header.data('filter-operator') || '',
                    name: header.data('filter-name') || '',
                    first: {
                        type: header.data('filter-first-type') || '',
                        val: header.data('filter-first-val') || ''
                    },
                    second: {
                        type: header.data('filter-second-type') || '',
                        val: header.data('filter-second-val') || ''
                    }
                },
                sort: {
                    isEnabled: header.data('sortable') == 'True',
                    firstOrder: header.data('sort-first') || '',
                    order: header.data('sort-order') || ''
                }
            };
        },
        set: function (options) {
            this.filters = $.extend(this.filters, options.filters);
            this.rowClicked = options.rowClicked || this.rowClicked;
            this.reloadEnded = options.reloadEnded || this.reloadEnded;
            this.reloadFailed = options.reloadFailed || this.reloadFailed;
            this.reloadStarted = options.reloadStarted || this.reloadStarted;

            if (options.reload === true) {
                this.reload(this.gridQuery);
            }
        },

        applyFiltering: function (column) {
            var grid = this;

            if (column.filter.isEnabled) {
                column.header.find('.mvc-grid-filter').on('click.mvcgrid', function () {
                    grid.renderFilter(column);
                });
            }
        },
        applySorting: function (column) {
            var grid = this;

            if (column.sort.isEnabled) {
                column.header.on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-filter').length == 0) {
                        grid.reload(grid.formSortQuery(column));
                    }
                });
            }
        },
        applyPaging: function (pageElement) {
            var page = pageElement.data('page') || '';
            var grid = this;

            if (page != '') {
                pageElement.on('click.mvcgrid', function () {
                    grid.reload(grid.formPageQuery(page));
                });
            }
        },

        reload: function (query) {
            var grid = this;

            if (grid.sourceUrl != '') {
                if (grid.reloadStarted) {
                    grid.reloadStarted(grid);
                }

                $.ajax({
                    cache: false,
                    url: grid.sourceUrl + '?' + query
                }).success(function (result) {
                    grid.element.hide();
                    grid.element.after(result);

                    var newGrid = grid.element.next('.mvc-grid').mvcgrid({
                        reloadStarted: grid.reloadStarted,
                        reloadFailed: grid.reloadFailed,
                        reloadEnded: grid.reloadEnded,
                        rowClicked: grid.rowClicked,
                        sourceUrl: grid.sourceUrl,
                        filters: grid.filters,
                        isLoaded: true,
                        query: query
                    }).data('mvc-grid');
                    grid.element.remove();

                    if (grid.reloadEnded) {
                        grid.reloadEnded(newGrid);
                    }
                })
                .error(function (result) {
                    if (grid.reloadFailed) {
                        grid.reloadFailed(grid, result);
                    }
                });
            } else {
                window.location.href = '?' + query;
            }
        },
        renderFilter: function (column) {
            var popup = $('body').children('.mvc-grid-popup');
            var gridFilter = this.filters[column.filter.name];

            if (gridFilter) {
                gridFilter.render(popup, column.filter);
                gridFilter.init(this, column, popup);

                this.setFilterPosition(column, popup);
                popup.addClass('open');

                $(window).on('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass('mvc-grid-filter') && target.parents('.mvc-grid-popup').length == 0 &&
                        !target.is('[class*="ui-datepicker"]') && target.parents('[class*="ui-datepicker"]').length == 0) {
                        $(window).off('click.mvcgrid');
                        popup.removeClass('open');
                    }
                });
            } else {
                $(window).off('click.mvcgrid');
                popup.removeClass('open');
            }
        },
        setFilterPosition: function (column, popup) {
            var filter = column.header.find('.mvc-grid-filter');
            var arrow = popup.find('.popup-arrow');
            var filterLeft = filter.offset().left;
            var filterTop = filter.offset().top;
            var filterHeight = filter.height();
            var winWidth = $(window).width();
            var popupWidth = popup.width();

            var popupTop = filterTop + filterHeight / 2 + 14;
            var popupLeft = filterLeft - 8;
            var arrowLeft = 15;

            if (filterLeft + popupWidth + 5 > winWidth) {
                popupLeft = winWidth - popupWidth - 14;
                arrowLeft = filterLeft - popupLeft + 7;
            }

            arrow.css('left', arrowLeft + 'px');
            popup.css('left', popupLeft + 'px');
            popup.css('top', popupTop + 'px');
        },

        formFilterQuery: function (column) {
            var secondKey = encodeURIComponent(this.name + '-' + column.name + '-' + column.filter.second.type);
            var firstKey = encodeURIComponent(this.name + '-' + column.name + '-' + column.filter.first.type);
            var operatorKey = encodeURIComponent(this.name + '-' + column.name + '-Op');
            var columnKey = encodeURIComponent(this.name + '-' + column.name + '-');
            var operatorValue = encodeURIComponent(column.filter.operator);
            var secondValue = encodeURIComponent(column.filter.second.val);
            var firstValue = encodeURIComponent(column.filter.first.val);
            var pageKey = encodeURIComponent(this.name + '-Page');
            var params = this.gridQuery.split('&');
            var secondParamExists = false;
            var firstParamExists = false;
            var operatorExists = false;
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var key = params[i].split('=')[0];
                if (params[i] != '' && key != pageKey) {
                    if (key.indexOf(columnKey) == 0) {
                        if (key == operatorKey && !operatorExists) {
                            if (!column.filter.isMulti) {
                                continue;
                            }

                            params[i] = key + '=' + operatorValue;
                            operatorExists = true;
                        } else if (!firstParamExists) {
                            params[i] = firstKey + '=' + firstValue;
                            firstParamExists = true;
                        } else if (firstParamExists && !secondParamExists) {
                            if (!column.filter.isMulti) {
                                continue;
                            }

                            params[i] = secondKey + '=' + secondValue;
                            secondParamExists = true;
                        }
                    }

                    newParams.push(params[i]);
                }
            }
            if (!firstParamExists) {
                newParams.push(firstKey + '=' + firstValue);
            }
            if (!operatorExists && column.filter.isMulti) {
                newParams.push(operatorKey + '=' + operatorValue);
            }
            if (!secondParamExists && column.filter.isMulti) {
                newParams.push(secondKey + '=' + secondValue);
            }

            return newParams.join('&');
        },
        formFilterQueryWithout: function (column) {
            var columnKey = encodeURIComponent(this.name + '-' + column.name + '-');
            var pageKey = encodeURIComponent(this.name + '-Page');
            var params = this.gridQuery.split('&');
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                var key = params[i].split('=')[0];
                if (params[i] != '' && key != pageKey && key.indexOf(columnKey) != 0) {
                    newParams.push(params[i]);
                }
            }

            return newParams.join('&');
        },
        formSortQuery: function (column) {
            var sortQuery = this.addOrReplace(this.gridQuery, this.name + '-Sort', column.name);
            var order = column.sort.order == 'Asc' ? 'Desc' : 'Asc';
            if (column.sort.order == '' && column.sort.firstOrder != '') {
                order = column.sort.firstOrder;
            }

            return this.addOrReplace(sortQuery, this.name + '-Order', order);
        },
        formPageQuery: function (page) {
            return this.addOrReplace(this.gridQuery, this.name + '-Page', page);
        },
        addOrReplace: function (query, key, value) {
            value = encodeURIComponent(value);
            key = encodeURIComponent(key);
            var params = query.split('&');
            var paramExists = false;
            var newParams = [];

            for (var i = 0; i < params.length; i++) {
                if (params[i] != '') {
                    var paramKey = params[i].split('=')[0];
                    if (paramKey == key) {
                        params[i] = key + '=' + value;
                        paramExists = true;
                    }

                    newParams.push(params[i]);
                }
            }
            if (!paramExists) {
                newParams.push(key + '=' + value);
            }

            return newParams.join('&');
        },

        bindGridEvents: function () {
            var grid = this;
            this.element.find('.mvc-grid-row').on('click.mvcgrid', function (e) {
                if (grid.rowClicked) {
                    var cells = $(this).find('td');
                    var data = [];

                    for (var ind = 0; ind < grid.columns.length; ind++) {
                        var column = grid.columns[ind];
                        if (cells.length > ind) {
                            data[column.name] = $(cells[ind]).text();
                        }
                    }

                    grid.rowClicked(grid, this, data, e);
                }
            });
        },

        cleanHeader: function (column) {
            var header = column.header;
            header.removeAttr('data-name');

            header.removeAttr('data-filterable');
            header.removeAttr('data-filter-name');
            header.removeAttr('data-filter-multi');
            header.removeAttr('data-filter-operator');
            header.removeAttr('data-filter-first-val');
            header.removeAttr('data-filter-first-type');
            header.removeAttr('data-filter-second-val');
            header.removeAttr('data-filter-second-type');

            header.removeAttr('data-sortable');
            header.removeAttr('data-sort-order');
            header.removeAttr('data-sort-first');
        },
        cleanGrid: function (grid) {
            grid.removeAttr('data-source-url');
            grid.removeAttr('data-name');
        }
    };

    return MvcGrid;
})();

var MvcGridTextFilter = (function () {
    function MvcGridTextFilter() {
    }

    MvcGridTextFilter.prototype = {
        render: function (popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter || $.fn.mvcgrid.defaultLang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Text;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.first.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="StartsWith"' + (filter.first.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.first.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                     '</div>' +
                     (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Contains"' + (filter.second.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Contains + '</option>' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="StartsWith"' + (filter.second.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.StartsWith + '</option>' +
                            '<option value="EndsWith"' + (filter.second.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.EndsWith + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                     '</div>' :
                     '') +
                     '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                     '</div>' +
                 '</div>');
        },

        init: function (grid, column, popup) {
            this.bindType(grid, column, popup);
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
            this.bindOperator(grid, column, popup);
        },
        bindType: function (grid, column, popup) {
            var firstType = popup.find('.first-filter .mvc-grid-type');
            firstType.on('change.mvcgrid', function () {
                column.filter.first.type = this.value;
            });
            firstType.change();

            var secondType = popup.find('.second-filter .mvc-grid-type');
            secondType.on('change.mvcgrid', function () {
                column.filter.second.type = this.value;
            });
            secondType.change();
        },
        bindValue: function (grid, column, popup) {
            var firstValue = popup.find('.first-filter .mvc-grid-input');
            firstValue.on('keyup.mvcgrid', function (e) {
                column.filter.first.val = this.value;
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });

            var secondValue = popup.find('.second-filter .mvc-grid-input');
            secondValue.on('keyup.mvcgrid', function (e) {
                column.filter.second.val = this.value;
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQuery(column));
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQueryWithout(column));
            });
        },
        bindOperator: function (grid, column, popup) {
            var operator = popup.find('.mvc-grid-operator');
            operator.on('change.mvcgrid', function () {
                column.filter.operator = this.value;
            });
        }
    };

    return MvcGridTextFilter;
})();

var MvcGridNumberFilter = (function () {
    function MvcGridNumberFilter() {
    }

    MvcGridNumberFilter.prototype = {
        render: function (popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter || $.fn.mvcgrid.defaultLang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Number;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        '<input class="mvc-grid-input" type="text" value="' + filter.second.val + '">' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindType(grid, column, popup);
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
            this.bindOperator(grid, column, popup);
        },
        bindType: function (grid, column, popup) {
            var firstType = popup.find('.first-filter .mvc-grid-type');
            firstType.on('change.mvcgrid', function () {
                column.filter.first.type = this.value;
            });
            firstType.change();

            var secondType = popup.find('.second-filter .mvc-grid-type');
            secondType.on('change.mvcgrid', function () {
                column.filter.second.type = this.value;
            });
            secondType.change();
        },
        bindValue: function (grid, column, popup) {
            var filter = this;

            var firstValue = popup.find('.first-filter .mvc-grid-input');
            firstValue.on('keyup.mvcgrid', function (e) {
                column.filter.first.val = this.value;
                if (filter.isValid(this.value)) {
                    $(this).removeClass('invalid');
                    if (e.which == 13) {
                        popup.find('.mvc-grid-apply').click();
                    }
                } else {
                    $(this).addClass('invalid');
                }
            });

            if (!filter.isValid(column.filter.first.val)) {
                firstValue.addClass('invalid');
            }

            var secondValue = popup.find('.second-filter .mvc-grid-input');
            secondValue.on('keyup.mvcgrid', function (e) {
                column.filter.second.val = this.value;
                if (filter.isValid(this.value)) {
                    $(this).removeClass('invalid');
                    if (e.which == 13) {
                        popup.find('.mvc-grid-apply').click();
                    }
                } else {
                    $(this).addClass('invalid');
                }
            });

            if (!filter.isValid(column.filter.second.val)) {
                secondValue.addClass('invalid');
            }
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQuery(column));
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQueryWithout(column));
            });
        },
        bindOperator: function (grid, column, popup) {
            var operator = popup.find('.mvc-grid-operator');
            operator.on('change.mvcgrid', function () {
                column.filter.operator = this.value;
            });
        },

        isValid: function (value) {
            if (value == '') return true;
            var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[.,]?\\d*$');

            return pattern.test(value);
        }
    };

    return MvcGridNumberFilter;
})();

var MvcGridDateFilter = (function () {
    function MvcGridDateFilter() {
    }

    MvcGridDateFilter.prototype = {
        render: function (popup, filter) {
            var filterInput = '<input class="mvc-grid-input" type="text" value="' + filter.first.val + '">';
            var filterLang = $.fn.mvcgrid.lang.Filter || $.fn.mvcgrid.defaultLang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Date;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.first.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.first.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.first.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.first.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.first.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="first-filter popup-group">' +
                        filterInput +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<select class="mvc-grid-type">' +
                            '<option value="Equals"' + (filter.second.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Equals + '</option>' +
                            '<option value="LessThan"' + (filter.second.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.LessThan + '</option>' +
                            '<option value="GreaterThan"' + (filter.second.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.GreaterThan + '</option>' +
                            '<option value="LessThanOrEqual"' + (filter.second.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.LessThanOrEqual + '</option>' +
                            '<option value="GreaterThanOrEqual"' + (filter.second.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.GreaterThanOrEqual + '</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="second-filter popup-group">' +
                        filterInput +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindType(grid, column, popup);
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
            this.bindOperator(grid, column, popup);
        },
        bindType: function (grid, column, popup) {
            var firstType = popup.find('.first-filter .mvc-grid-type');
            firstType.on('change.mvcgrid', function () {
                column.filter.first.type = this.value;
            });
            firstType.change();

            var secondType = popup.find('.second-filter .mvc-grid-type');
            secondType.on('change.mvcgrid', function () {
                column.filter.second.type = this.value;
            });
            secondType.change();
        },
        bindValue: function (grid, column, popup) {
            var firstValue = popup.find('.first-filter .mvc-grid-input');
            if ($.fn.datepicker) { firstValue.datepicker(); }

            firstValue.on('change.mvcgrid keyup.mvcgrid', function (e) {
                column.filter.first.val = this.value;
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });

            var secondValue = popup.find('.second-filter .mvc-grid-input');
            if ($.fn.datepicker) { secondValue.datepicker(); }

            secondValue.on('change.mvcgrid keyup.mvcgrid', function (e) {
                column.filter.second.val = this.value;
                if (e.which == 13) {
                    popup.find('.mvc-grid-apply').click();
                }
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQuery(column));
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQueryWithout(column));
            });
        },
        bindOperator: function (grid, column, popup) {
            var operator = popup.find('.mvc-grid-operator');
            operator.on('change.mvcgrid', function () {
                column.filter.operator = this.value;
            });
        }
    };

    return MvcGridDateFilter;
})();

var MvcGridBooleanFilter = (function () {
    function MvcGridBooleanFilter() {
    }

    MvcGridBooleanFilter.prototype = {
        render: function (popup, filter) {
            var filterLang = $.fn.mvcgrid.lang.Filter || $.fn.mvcgrid.defaultLang.Filter;
            var operator = $.fn.mvcgrid.lang.Operator;
            var lang = $.fn.mvcgrid.lang.Boolean;

            popup.html(
                '<div class="popup-arrow"></div>' +
                '<div class="popup-content">' +
                    '<div class="first-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.first.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.first.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' +
                    (filter.isMulti ?
                     '<div class="popup-group popup-group-operator">' +
                        '<select class="mvc-grid-operator">' +
                            '<option value="">' + operator.Select + '</option>' +
                            '<option value="And"' + (filter.operator == 'And' ? ' selected="selected"' : '') + '>' + operator.And + '</option>' +
                            '<option value="Or"' + (filter.operator == 'Or' ? ' selected="selected"' : '') + '>' + operator.Or + '</option>' +
                        '</select>' +
                     '</div>' +
                     '<div class="second-filter popup-group">' +
                        '<ul class="mvc-grid-boolean-filter">' +
                            '<li ' + (filter.second.val == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Yes + '</li>' +
                            '<li ' + (filter.second.val == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.No + '</li>' +
                        '</ul>' +
                    '</div>' :
                     '') +
                    '<div class="popup-button-group">' +
                        '<button class="btn btn-success mvc-grid-apply" type="button">' + filterLang.Apply + '</button>' +
                        '<button class="btn btn-danger mvc-grid-cancel" type="button">' + filterLang.Remove + '</button>' +
                    '</div>' +
                '</div>');
        },

        init: function (grid, column, popup) {
            this.bindValue(grid, column, popup);
            this.bindApply(grid, column, popup);
            this.bindCancel(grid, column, popup);
            this.bindOperator(grid, column, popup);
        },
        bindValue: function (grid, column, popup) {
            var firstValues = popup.find('.first-filter .mvc-grid-boolean-filter li');
            column.filter.first.type = 'Equals';

            firstValues.on('click.mvcgrid', function () {
                var item = $(this);

                column.filter.first.val = item.data('value');
                item.siblings().removeClass('active');
                item.addClass('active');
            });

            var secondValues = popup.find('.second-filter .mvc-grid-boolean-filter li');
            column.filter.second.type = 'Equals';

            secondValues.on('click.mvcgrid', function () {
                var item = $(this);

                column.filter.second.val = item.data('value');
                item.siblings().removeClass('active');
                item.addClass('active');
            });
        },
        bindApply: function (grid, column, popup) {
            var apply = popup.find('.mvc-grid-apply');
            apply.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQuery(column));
            });
        },
        bindCancel: function (grid, column, popup) {
            var cancel = popup.find('.mvc-grid-cancel');
            cancel.on('click.mvcgrid', function () {
                popup.removeClass('open');
                grid.reload(grid.formFilterQueryWithout(column));
            });
        },
        bindOperator: function (grid, column, popup) {
            var operator = popup.find('.mvc-grid-operator');
            operator.on('change.mvcgrid', function () {
                column.filter.operator = this.value;
            });
        }
    };

    return MvcGridBooleanFilter;
})();

$.fn.mvcgrid = function (options) {
    return this.each(function () {
        if (!$.data(this, 'mvc-grid')) {
            $.data(this, 'mvc-grid', new MvcGrid($(this), options));
        } else if (options) {
            $.data(this, 'mvc-grid').set(options);
        }
    });
};
$.fn.mvcgrid.defaultLang = {
    Text: {
        Contains: 'Contains',
        Equals: 'Equals',
        StartsWith: 'Starts with',
        EndsWith: 'Ends with'
    },
    Number: {
        Equals: 'Equals',
        LessThan: 'Less than',
        GreaterThan: 'Greater than',
        LessThanOrEqual: 'Less than or equal',
        GreaterThanOrEqual: 'Greater than or equal'
    },
    Date: {
        Equals: 'Equals',
        LessThan: 'Is before',
        GreaterThan: 'Is after',
        LessThanOrEqual: 'Is before or equal',
        GreaterThanOrEqual: 'Is after or equal'
    },
    Boolean: {
        Yes: 'Yes',
        No: 'No'
    },
    Filter: {
        Apply: '&#10004;',
        Remove: '&#10008;'
    },
    Operator: {
        Select: '',
        And: 'and',
        Or: 'or'
    }
};
$.fn.mvcgrid.lang = $.fn.mvcgrid.defaultLang;
$(function () {
    $('body').append('<div class="mvc-grid-popup"></div>');
    $(window).resize(function () {
        $('.mvc-grid-popup').removeClass('open');
    });
});
