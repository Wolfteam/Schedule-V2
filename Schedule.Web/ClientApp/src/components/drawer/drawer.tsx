import React, { PureComponent } from 'react'
import {
    Avatar,
    Collapse,
    createStyles,
    Divider,
    Grid,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    SwipeableDrawer,
    Theme,
    WithStyles,
    withStyles
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
import { RouteComponentProps, withRouter } from 'react-router-dom';
import * as routes from '../../routes';
import { AuthContext } from '../../contexts/auth-context';
import { logout } from '../../services/account.service'

const styles = (theme: Theme) => createStyles({
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
});

interface Props extends RouteComponentProps<any>, WithStyles<typeof styles> {
    readonly isDrawerOpen: boolean,
    readonly onDrawerStateChanged: (isOpen: boolean) => void;
}

interface State {
    readonly expanded: Map<number, boolean>;
    readonly keyPressed: Map<string, boolean>;
}

interface DrawerItem {
    readonly id: number;
    readonly text: string;
    readonly subItems: DrawerItem[];
    readonly icon: IconProp;
    readonly route: string;
}

class Drawer extends PureComponent<Props, State> {
    state: State = {
        expanded: new Map<number, boolean>(),
        keyPressed: new Map<string, boolean>(Object.entries({
            "Control": false,
            "Shift": false
        }))
    };
    static contextType = AuthContext;
    context!: React.ContextType<typeof AuthContext>;

    handleKeyPress = (isKeyDown: boolean) => (e: KeyboardEvent) => {
        const key = e.key;
        const keys = Array.from(this.state.keyPressed.keys());
        const [authContext] = this.context;

        if (!keys.includes(key) || !authContext.isAuthenticated) {
            return;
        }

        const keyPressed = this.state.keyPressed.set(key, isKeyDown);
        const allWerePressed = Array.from(keyPressed.values()).every(e => e);

        if (allWerePressed && isKeyDown) {
            this.props.onDrawerStateChanged(true);
        }
        this.setState({ keyPressed });
    };

    toggleDrawer = (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
        if (
            event &&
            event.type === 'keydown' &&
            ((event as React.KeyboardEvent).key === 'Tab' || (event as React.KeyboardEvent).key === 'Shift')
        ) {
            return;
        }
        this.props.onDrawerStateChanged(open);
    };

    handleExpandedItemClick = (item: DrawerItem): void => {
        let expandedState = new Map<number, boolean>(this.state.expanded);
        let expanded = true;
        if (expandedState.has(item.id)) {
            expanded = !expandedState.get(item.id) ?? true;
            expandedState.delete(item.id);
        }
        expandedState = expandedState.set(item.id, expanded);
        this.setState({ expanded: expandedState });
    };

    handleItemClick(item: DrawerItem, isParent: boolean): void {
        if (item.subItems.length === 0 || !isParent) {
            this.props.onDrawerStateChanged(false);
            const { history } = this.props!;
            history.push(item.route);
            return;
        }

        this.handleExpandedItemClick(item);
    };

    buildItem = (item: DrawerItem, isParent: boolean = true) => {
        const hasSubItems = item.subItems.length > 0;
        const areItemsExpanded = this.state.expanded.get(item.id) ?? false;

        const expandedControl = hasSubItems
            ? areItemsExpanded
                ? <ExpandLess />
                : <ExpandMore />
            : null;

        const subItems = item.subItems.map((subItem) => {
            return this.buildItem(subItem, false);
        });

        const childClass = isParent ? '' : this.props.classes.nestedListItem;
        const parent = <ListItem button
            key={item.id}
            onClick={() => this.handleItemClick(item, isParent)}
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
        </React.Fragment>;
    }

    signOut = async () => {
        await logout();
        const [authContext, setAuthContext] = this.context;
        this.props.onDrawerStateChanged(false);
        setAuthContext({ isAuthenticated: false, username: '' });
    }

    componentDidMount() {
        document.addEventListener('keydown', this.handleKeyPress(true));
        document.addEventListener('keyup', this.handleKeyPress(false));
    }

    componentWillUnmount() {
        document.removeEventListener('keydown', this.handleKeyPress(true));
        document.removeEventListener('keyup', this.handleKeyPress(false));
    }

    render() {
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
                text: translations.loadAvailability,
                icon: faTasks,
                subItems: [],
                route: routes.availabilityPath
            },
            {
                id: 3,
                text: translations.others,
                icon: faDatabase,
                route: '',
                subItems: [
                    {
                        id: 5,
                        text: translations.classrooms,
                        icon: faSchool,
                        subItems: [],
                        route: routes.classRoomsPath
                    },
                    {
                        id: 6,
                        text: translations.careers,
                        icon: faGraduationCap,
                        subItems: [],
                        route: routes.careersPath
                    },
                    {
                        id: 7,
                        text: translations.subjects,
                        icon: faChalkboard,
                        subItems: [],
                        route: routes.subjectsPath
                    },
                    {
                        id: 8,
                        text: translations.careerPeriod,
                        icon: faCalendarDay,
                        subItems: [],
                        route: routes.careersPeriodPath
                    },
                    {
                        id: 9,
                        text: translations.priorities,
                        icon: faExclamationCircle,
                        subItems: [],
                        route: routes.prioritiesPath
                    },
                    {
                        id: 10,
                        text: translations.teachers,
                        icon: faChalkboardTeacher,
                        subItems: [],
                        route: routes.teachersPath
                    },
                    {
                        id: 11,
                        text: `${translations.teachers} - ${translations.subjects}`,
                        icon: faAddressBook,
                        subItems: [],
                        route: routes.teachersPerSubjectsPath
                    },
                    {
                        id: 12,
                        text: translations.sections,
                        icon: faProjectDiagram,
                        subItems: [],
                        route: routes.sectionsPath
                    },
                    {
                        id: 13,
                        text: translations.semesters,
                        icon: faHourglassStart,
                        subItems: [],
                        route: routes.semestersPath
                    },
                    {
                        id: 14,
                        text: `${translations.classrooms} - ${translations.subjects}`,
                        icon: faAddressCard,
                        subItems: [],
                        route: routes.sbujectClassroomTypePath
                    },
                    {
                        id: 15,
                        text: translations.users,
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

        const listItems = items.map((item, index) => this.buildItem(item));

        return <React.Fragment>
            <SwipeableDrawer
                anchor='left'
                open={this.props.isDrawerOpen}
                onClose={this.toggleDrawer(false)}
                onOpen={this.toggleDrawer(true)}>
                <Grid container alignItems="center" style={{ minHeight: '120px', minWidth: '320px' }}>
                    <Grid item className={this.props.classes.avatarBg}>
                        <Avatar className={this.props.classes.largeAvatar} alt="Cindy Baker" src="https://avatars3.githubusercontent.com/u/1976115?s=400&u=075b0e713462b1649c2123a1f1ffeca8b11c8783&v=4" />
                        <p className={this.props.classes.fullName}>Efrain Bastidas Berrios</p>
                        <p className={this.props.classes.username}>Wolfteam20</p>
                    </Grid>
                </Grid>
                <Divider />
                <List>
                    {listItems}
                </List>
                <Divider />
                <ListItem button onClick={this.signOut}>
                    <ListItemIcon><FontAwesomeIcon size="2x" icon={faSignOutAlt} /></ListItemIcon>
                    <ListItemText primary={translations.logout} />
                </ListItem>
            </SwipeableDrawer>
        </React.Fragment>;
    }
}

export default withStyles(styles)(withRouter(Drawer));