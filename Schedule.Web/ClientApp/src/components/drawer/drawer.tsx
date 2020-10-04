import React from 'react'
import {
    Accordion,
    AccordionSummary,
    Avatar,
    Button,
    Collapse,
    createStyles,
    Divider,
    Grid,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    makeStyles,
    SwipeableDrawer,
    Theme,
    Typography
} from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {
    faHome,
    faDatabase,
    faTasks,
    faLock,
    faSignOutAlt,
    faSchool,
    faGraduationCap,
    faChalkboard,
    faChalkboardTeacher,
    faAddressCard,
    faAddressBook,
    faUsers,
    faProjectDiagram,
    faHourglassStart,
    faExclamationCircle,
    faCalendarDay
} from '@fortawesome/free-solid-svg-icons'
import translations from '../../services/translations'
import { ExpandLess, ExpandMore } from '@material-ui/icons';
import { IconProp } from '@fortawesome/fontawesome-svg-core';
import { useHistory } from 'react-router-dom';
import * as routes from '../../routes';

interface Props {
    isDrawerOpen: boolean,
    onDrawerStateChanged: (isOpen: boolean) => void
}

interface State {
    expanded: Map<number, boolean>;
}

interface DrawerItem {
    id: number;
    text: string;
    subItems: DrawerItem[];
    icon: IconProp;
    route: string;
}

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        fullName: {
            margin: '10px 0px 0px 10px'
        },
        username: {
            margin: '0px 0px 0px 10px'
        },
        avatarBg: {
            color: 'white',
            backgroundPosition: 'center',
            backgroundRepeat: 'no-repeat',
            width: '100%',
            backgroundImage: 'url(https://scontent.fuio7-1.fna.fbcdn.net/v/t31.0-8/1049096_10201132489971690_72266505_o.jpg?_nc_cat=100&_nc_sid=e3f864&_nc_eui2=AeEDI9ZbJ9xZ2PYvILX3pHNyE-9tPqnKST4T720-qcpJPnQmr1b1-mk_UOluDKOvkrY&_nc_ohc=b4Nkl8VneNsAX8bKKNn&_nc_ht=scontent.fuio7-1.fna&oh=7b69e78d5466d234dc10e3e25c1100e2&oe=5F9A5974)'
        },
        largeAvatar: {
            width: theme.spacing(10),
            height: theme.spacing(10),
        },
        nestedListItem: {
            paddingLeft: theme.spacing(4),
        },
    }),
);

function Drawer(props: Props) {
    const classes = useStyles();
    const [state, setState] = React.useState<State>({
        expanded: new Map<number, boolean>()
    });
    const history = useHistory();

    const toggleDrawer = (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
        if (
            event &&
            event.type === 'keydown' &&
            ((event as React.KeyboardEvent).key === 'Tab' || (event as React.KeyboardEvent).key === 'Shift')
        ) {
            return;
        }
        props.onDrawerStateChanged(open);
    };

    const handleExpandedItemClick = (item: DrawerItem) => {
        let expandedState = state.expanded;
        let expanded = true;
        if (expandedState.has(item.id)) {
            expanded = !expandedState.get(item.id) ?? true;
            expandedState.delete(item.id);
        }
        expandedState = expandedState.set(item.id, expanded);
        setState({ expanded: expandedState });
    };

    const handleItemClick = (item: DrawerItem, isParent: boolean) => {
        if (item.subItems.length === 0 || !isParent) {
            console.log(`Pushing to route = ${item.route}`);
            props.onDrawerStateChanged(false);
            history.push(item.route);
            return;
        }

        handleExpandedItemClick(item);
    };

    const buildItem = (item: DrawerItem, isParent: boolean = true) => {
        const hasSubItems = item.subItems.length > 0;
        const areItemsExpanded = state.expanded.get(item.id) ?? false;

        const expandedControl = hasSubItems
            ? areItemsExpanded
                ? <ExpandLess />
                : <ExpandMore />
            : null;

        const subItems = item.subItems.map((subItem) => {
            return buildItem(subItem, false);
        });

        const childClass = isParent ? '' : classes.nestedListItem;
        const parent = <ListItem button
            key={item.id}
            onClick={() => handleItemClick(item, isParent)}
            className={childClass}>
            <ListItemIcon>
                <FontAwesomeIcon icon={item.icon} size="2x" />
            </ListItemIcon>
            <ListItemText primary={item.text} />
            {expandedControl}
        </ListItem>;

        if (!hasSubItems) {
            return parent;
        }

        const childKey = `child-${item.id}`;
        return <React.Fragment key={childKey}>
            {parent}
            <Collapse in={areItemsExpanded} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                    {subItems}
                </List>
            </Collapse>
        </React.Fragment>
    }

    const items: DrawerItem[] = [
        {
            id: 1,
            text: translations.home,
            icon: faHome,
            subItems: [],
            route: routes.HomePath
        },
        {
            id: 2,
            text: 'Cargar Disponibilidad',
            icon: faTasks,
            subItems: [],
            route: routes.availabilityPath
        },
        {
            id: 3,
            text: 'Editar Base de Datos',
            icon: faDatabase,
            route: '',
            subItems: [
                {
                    id: 5,
                    text: 'Aulas',
                    icon: faSchool,
                    subItems: [],
                    route: routes.classRoomsPath
                },
                {
                    id: 6,
                    text: 'Carreras',
                    icon: faGraduationCap,
                    subItems: [],
                    route: routes.careersPath
                },
                {
                    id: 7,
                    text: 'Materias',
                    icon: faChalkboard,
                    subItems: [],
                    route: routes.subjectsPath
                },
                {
                    id: 8,
                    text: 'Periodo Carrera',
                    icon: faCalendarDay,
                    subItems: [],
                    route: routes.careersPeriodPath
                },
                {
                    id: 9,
                    text: 'Prioridades',
                    icon: faExclamationCircle,
                    subItems: [],
                    route: routes.prioritiesPath
                },
                {
                    id: 10,
                    text: 'Profesores',
                    icon: faChalkboardTeacher,
                    subItems: [],
                    route: routes.teachersPath
                },
                {
                    id: 11,
                    text: 'Profesores - Materias',
                    icon: faAddressBook,
                    subItems: [],
                    route: routes.teachersPerSubjectsPath
                },
                {
                    id: 12,
                    text: 'Secciones',
                    icon: faProjectDiagram,
                    subItems: [],
                    route: routes.sectionsPath
                },
                {
                    id: 13,
                    text: 'Semestres',
                    icon: faHourglassStart,
                    subItems: [],
                    route: routes.semestersPath
                },
                {
                    id: 14,
                    text: 'Tipo Aula - Materia',
                    icon: faAddressCard,
                    subItems: [],
                    route: routes.sbujectClassroomTypePath
                },
                {
                    id: 15,
                    text: 'Usuarios',
                    icon: faUsers,
                    subItems: [],
                    route: routes.usersPath
                },
            ]
        },
        {
            id: 4,
            text: translations.changePassword,
            icon: faLock,
            subItems: [],
            route: routes.ChangePasswordPath
        }
    ];

    const listItems = items.map((item, index) => buildItem(item));

    return <div>
        <React.Fragment>
            <SwipeableDrawer
                anchor='left'
                open={props.isDrawerOpen}
                onClose={toggleDrawer(false)}
                onOpen={toggleDrawer(true)}
                style={{ minWidth: '300px' }}>
                <div
                    role="presentation"
                // onClick={toggleDrawer(false)}
                // onKeyDown={toggleDrawer(false)}
                >
                    <Grid container alignItems="center" style={{ minHeight: '120px' }}>
                        <Grid item className={classes.avatarBg}>
                            <Avatar className={classes.largeAvatar} alt="Cindy Baker" src="https://avatars3.githubusercontent.com/u/1976115?s=400&u=075b0e713462b1649c2123a1f1ffeca8b11c8783&v=4" />
                            <p className={classes.fullName}>Efrain Bastidas Berrios</p>
                            <p className={classes.username}>Wolfteam20</p>
                        </Grid>
                    </Grid>
                    <Divider />
                    <List>
                        {listItems}
                    </List>
                    <Divider />
                    <ListItem button >
                        <ListItemIcon><FontAwesomeIcon size="2x" icon={faSignOutAlt} /></ListItemIcon>
                        <ListItemText primary="Cerrar Sesión" />
                    </ListItem>
                </div>
            </SwipeableDrawer>
        </React.Fragment>
    </div>
}

export default Drawer;