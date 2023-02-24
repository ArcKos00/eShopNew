import { useContext, useState } from 'react';
import {
    Avatar,
    Link,
    IconButton,
    Menu,
    MenuItem,
    Typography,
    Button
} from '@mui/material';
import { AppStoreContext } from '../../App';
import ExitToApp from '@mui/icons-material/ExitToApp';
import { routes } from '../../routes';
import { NavLink, useNavigate } from 'react-router-dom';
import LoginStore from '../../pages/Login/LoginStore';
import { observer } from 'mobx-react-lite';
import AuthService from '../../stores/AuthService';

var authService = new AuthService();

const UserAuth = () => {
    const [anchorElUser, setAnchorElUser] = useState(null);
    const login = routes.find(item => item.key == 'login-user');
    const register = routes.find(item => item.key == 'register-user');

    const app = useContext(AppStoreContext);
    const log = new LoginStore(app.authStore);
    const navigate = useNavigate();

    const handleOpenUserMenu = (event: any) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const logout = () => {
        log.logout();
        navigate('/');
    };

    return (
        <>{!!app.authStore.token ?
            (<>
                <IconButton
                    size='large'
                    aria-label='account of current user'
                    aria-controls='user-appbar'
                    aria-haspopup='true'
                    onClick={handleOpenUserMenu}
                    color='primary'
                >
                    <Avatar />
                </IconButton>
                <Menu
                    id='user-appbar'
                    anchorEl={anchorElUser}
                    anchorOrigin={{
                        vertical: 'bottom',
                        horizontal: 'left'
                    }}
                    keepMounted
                    transformOrigin={{
                        vertical: 'top',
                        horizontal: 'left'
                    }}
                    open={Boolean(anchorElUser)}
                    onClose={handleCloseUserMenu}
                    sx={{
                        display: 'block'
                    }}>
                    {routes.map((page) => (
                        (page.place == 'UserMenu' && !!page.enabled) && <Link
                            key={page.key}
                            component={NavLink}
                            to={page.path}
                            color='black'
                            underline='none'
                            variant='button'
                        >
                            <MenuItem onClick={handleCloseUserMenu}>
                                <Typography textAlign='center'>
                                    {page.title}
                                </Typography>
                            </MenuItem>
                        </Link>
                    ))}
                </Menu>
                <Button onClick={logout}>
                    <ExitToApp />
                </Button>
            </>
            ) : (
                <>
                    <Button onClick={authService.userManager.signinPopup} variant='contained' color='success'>
                        asdasd
                    </Button>
                </>
            )}
            <Button onClick={authService.userManager.signinRedirect}>
                asdasd
            </Button>
        </>
    );
};

export default observer(UserAuth);