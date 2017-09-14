import { RouteInfo } from './sidebar.metadata';

export const ROUTES: RouteInfo[] = [
    { id: 'todo', path: '/home/todo', title: 'To-Do', miniTitle: '', icon: 'playlist_add_check' },
    { id: 'dashboard', path: '/home/dashboard', title: 'Dashboard', miniTitle: '', icon: 'dashboard' },

    {
        id: 'components', path: '', title: 'Components', miniTitle: '', icon: 'apps', children: [
            { id: 'components_buttons', path: '/components/buttons', title: 'Buttons', miniTitle: 'B', icon: '' },
            { id: 'components_grid', path: '/components/grid', title: 'Grid System', miniTitle: 'GS', icon: '' },
            { id: 'components_panels', path: '/components/panels', title: 'Panels', miniTitle: 'P', icon: '' },
            { id: 'components_sweet_alert', path: '/components/sweet-alert', title: 'Sweet Alert', miniTitle: 'SA', icon: '' },
            { id: 'components_notifications', path: '/components/notifications', title: 'Notifications', miniTitle: 'N', icon: '' },
            { id: 'components_icons', path: '/components/icons', title: 'Icons', miniTitle: 'I', icon: '' },
            { id: 'components_typography', path: '/components/typography', title: 'Typography', miniTitle: 'T', icon: '' },
        ]
    },

    {
        id: 'forms', path: '', title: 'Forms', miniTitle: '', icon: 'content_paste', children: [
            { id: 'forms_regular', path: '/forms/regular', title: 'Regular Forms', miniTitle: 'RF', icon: '' },
            { id: 'forms_extended', path: '/forms/extended', title: 'Extended Forms', miniTitle: 'EF', icon: '' },
            { id: 'forms_validation', path: '/forms/validation', title: 'Validation Forms', miniTitle: 'VF', icon: '' },
            { id: 'forms_wizard', path: '/forms/wizard', title: 'Wizard', miniTitle: 'W', icon: '' },
        ]
    },

    {
        id: 'tables', path: '', title: 'Tables', miniTitle: '', icon: 'grid_on', children: [
            { id: 'tables_regular', path: '/tables/regular', title: 'Regular Tables', miniTitle: 'RT', icon: '' },
            { id: 'tables_extended', path: '/tables/extended', title: 'Extended Tables', miniTitle: 'ET', icon: '' },
            { id: 'tables_datatables_net', path: '/tables/datatables.net', title: 'DataTables.net', miniTitle: 'DT', icon: '' },
        ]
    },

    {
        id: 'maps', path: '', title: 'Maps', miniTitle: '', icon: 'place', children: [
            { id: 'maps_google', path: '/maps/google', title: 'Google Maps', miniTitle: 'GM', icon: '' },
            { id: 'maps_full_screen', path: '/maps/fullscreen', title: 'Full Screen Map', miniTitle: 'FSM', icon: '' },
            { id: 'maps_vector', path: '/maps/vector', title: 'Vector Map', miniTitle: 'VM', icon: '' },
        ]
    },

    { id: 'widgets', path: '/widgets', title: 'Widgets', miniTitle: '', icon: 'widgets' },

    { id: 'charts', path: '/charts', title: 'Charts', miniTitle: '', icon: 'timeline' },

    { id: 'calendar', path: '/calendar', title: 'Calendar', miniTitle: '', icon: 'date_range' },

    {
        id: 'pages', path: '', title: 'Pages', miniTitle: '', icon: 'image', children: [
            { id: 'pages_pricing', path: '/pages/pricing', title: 'Pricing', miniTitle: 'P', icon: '' },
            { id: 'pages_timeline', path: '/pages/timeline', title: 'Timeline Page', miniTitle: 'TP', icon: '' },
            { id: 'pages_user', path: '/pages/user', title: 'User Page', miniTitle: 'UP', icon: '' },
        ]
    },
];
