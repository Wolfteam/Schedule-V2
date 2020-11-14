import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    FormControl,
    Grid,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    TextField
} from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import translations from '../../services/translations';

const rowsToTake = [5, 10, 20, 50];

interface State {
    itemsPerPage: number;
    searchTerm: string;
}

interface Props {
    showSearch: boolean;
    showItemsPerPage: boolean;
    searchText: string;
    isBusy: boolean;
    onItemsPerPageChanged?: (take: number) => void;
    onSearchTermChanged?: (newVal: string) => void;
}

function CustomTableSearch(props: Props) {
    const [state, setState] = useState<State>({
        itemsPerPage: rowsToTake[0],
        searchTerm: props.searchText
    });

    const { onSearchTermChanged } = props;

    useEffect(() => {
        const timeout = setTimeout(async () => {
            if (state.searchTerm !== props.searchText && onSearchTermChanged)
                onSearchTermChanged(state.searchTerm);
        }, 500);

        return () => clearTimeout(timeout);
    }, [onSearchTermChanged, props.searchText, state.searchTerm]);

    const menuItems = rowsToTake.map(el => <MenuItem key={el} value={el}>{el}</MenuItem>);
    const itemsPerPageChanged = (newVal: number) => {
        if (state.itemsPerPage !== newVal) {
            setState({ ...state, itemsPerPage: newVal });
            if (props.onItemsPerPageChanged)
                props.onItemsPerPageChanged(newVal);
        }
    };

    const searchTermChanged = (newVal: string) => {
        setState({ ...state, searchTerm: newVal });
    };

    const searchIcon = <InputAdornment position="start" >
        <FontAwesomeIcon icon={faSearch} />
    </InputAdornment>;

    const itemsPerPage = props.showItemsPerPage ? <FormControl fullWidth>
        <InputLabel id="rows-to-take">{translations.itemsPerPage}</InputLabel>
        <Select
            labelId="rows-to-take"
            value={state.itemsPerPage}
            fullWidth
            onChange={(e) => itemsPerPageChanged(e.target.value as number)}>
            {menuItems}
        </Select>
    </FormControl> : null;

    const search = props.showSearch ? <TextField key="search"
        variant="outlined"
        margin="normal"
        fullWidth
        required
        size="small"
        disabled={props.isBusy}
        label={translations.search}
        onChange={(e) => searchTermChanged(e.target.value)}
        type="text"
        InputProps={{
            startAdornment: (searchIcon)
        }} /> : null;

    return <Grid container justify="space-between" alignItems="center">
        <Grid item xs={12} sm={3}>
            {itemsPerPage}
        </Grid>
        <Grid item xs={12} sm={6}></Grid>
        <Grid item xs={12} sm={3}>
            {search}
        </Grid>
    </Grid>;
}

export default React.memo(CustomTableSearch);
