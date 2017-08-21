using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This list was taken from https://github.com/rheit/zdoom/blob/master/wadsrc/static/filter/game-doomchex/sndinfo.txt
//and modified for use with UnityDoom


public class SoundInfo : MonoBehaviour
{
    public Dictionary<string, string> soundInfo = new Dictionary<string, string>
    {

        {"misc/unused", "dsskldth" },

        /*
        $playersound player  death      dspldeth
        $playersound player  xdeath     dspdiehi
        $playersound player gibbed     dsslop
        $playersound player pain100    dsplpain
        $playersounddup player  pain75		*pain100
        $playersounddup player  pain50		*pain100
        $playersounddup player  pain25		*pain100
        $playersound player  grunt      dsoof
        $playersounddup player  land		*grunt
        $playersound player  jump       dsjump
        $playersound player  fist       dspunch
        $playersound player  usefail    dsnoway
        */


        // Weapons

        {"weapons/sawup", "dssawup" },
        {"weapons/sawidle", "dssawidl" },
        {"weapons/sawfull", "dssawful" },
        {"weapons/sawhit", "dssawhit" },

        {"weapons/pistol", "dspistol" },
        {"weapons/shotgf", "dsshotgn" },
        {"weapons/shotgr", "dssgcock" },
        {"weapons/sshotf", "dsdshtgn" },
        {"weapons/sshoto", "dsdbopn" },
        {"weapons/sshotc", "dsdbcls" },
        {"weapons/sshotl", "dsdbload" },
        {"weapons/chngun", "dspistol" },
        {"weapons/rocklx", "dsbarexp" },
        {"weapons/rocklf", "dsrlaunc" },
        {"weapons/plasmaf", "dsplasma" },
        {"weapons/plasmax", "dsfirxpl" },
        {"weapons/bfgf", "dsbfg" },
        {"weapons/bfgx", "dsrxplod" },
        {"weapons/railgf", "railgf1" },
        {"weapons/grbnce", "dsbounce" },
        {"weapons/grenlx", "dsgrnexp" },
        {"weapons/grenlf", "dsglaunc" },

        //===========================================================================
        //
        // MONSTER SOUNDS
        //
        //===========================================================================

        {"misc/gibbed", "dsslop" },

        // Zombie man

        {"grunt/sight", "grunt/sight1 grunt/sight2 grunt/sight3" },
        {"grunt/death", "grunt/death1 grunt/death2 grunt/death3" },
        {"grunt/sight1", "dsposit1" },
        {"grunt/sight2", "dsposit2" },
        {"grunt/sight3", "dsposit3" },
        {"grunt/active", "dsposact" },
        {"grunt/pain", "dspopain" },
        {"grunt/death1", "dspodth1" },
        {"grunt/death2", "dspodth2" },
        {"grunt/death3", "dspodth3" },
        {"grunt/attack", "dspistol" },

        // Shotgun guy

        {"shotguy/sight", "shotguy/sight1 shotguy/sight2 shotguy/sight3" },
        {"shotguy/death", "shotguy/death1 shotguy/death2 shotguy/death3" },
        {"shotguy/sight1", "dsposit1" },
        {"shotguy/sight2", "dsposit2" },
        {"shotguy/sight3", "dsposit3" },
        {"shotguy/active", "dsposact" },
        {"shotguy/pain", "dspopain" },
        {"shotguy/death1", "dspodth1" },
        {"shotguy/death2", "dspodth2" },
        {"shotguy/death3", "dspodth3" },
        {"shotguy/attack", "dsshotgn" },

        // Archvile

        {"vile/sight", "dsvilsit" },
        {"vile/active", "dsvilact" },
        {"vile/pain", "dsvipain" },
        {"vile/death", "dsvildth" },
        {"vile/raise", "dsslop" },
        {"vile/start", "dsvilatk" },
        {"vile/stop", "dsbarexp" },
        {"vile/firestrt", "dsflamst" },
        {"vile/firecrkl", "dsflame" },

        // Revenant

        {"skeleton/sight", "dsskesit" },
        {"skeleton/active", "dsskeact" },
        {"skeleton/pain", "dspopain" },
        {"skeleton/melee", "dsskepch" },
        {"skeleton/swing", "dsskeswg" },
        {"skeleton/death", "dsskedth" },
        {"skeleton/attack", "dsskeatk" },
        {"skeleton/tracex", "dsbarexp" },

        // Fatso

        {"fatso/sight", "dsmansit" },
        {"fatso/active", "dsposact" },
        {"fatso/pain", "dsmnpain" },
        {"fatso/raiseguns", "dsmanatk" },
        {"fatso/death", "dsmandth" },
        {"fatso/attack", "dsfirsht" },
        {"fatso/shotx", "dsfirxpl" },

        // Chainguy

        {"chainguy/sight", "chainguy/sight1 chainguy/sight2 chainguy/sight3" },
        {"chainguy/death", "chainguy/death1 chainguy/death2 chainguy/death3" },
        {"chainguy/sight1", "dsposit1" },
        {"chainguy/sight2", "dsposit2" },
        {"chainguy/sight3", "dsposit3" },
        {"chainguy/active", "dsposact" },
        {"chainguy/pain", "dspopain" },
        {"chainguy/death1", "dspodth1" },
        {"chainguy/death2", "dspodth2" },
        {"chainguy/death3", "dspodth3" },
        {"chainguy/attack", "dsshotgn" },

        // Imp

        {"imp/sight", "imp/sight1 imp/sight2" },
        {"imp/death", "imp/death1 imp/death2" },
        {"imp/sight1", "dsbgsit1" },
        {"imp/sight2", "dsbgsit2" },
        {"imp/active", "dsbgact" },
        {"imp/pain", "dspopain" },
        {"imp/melee", "dsclaw" },
        {"imp/death1", "dsbgdth1" },
        {"imp/death2", "dsbgdth2" },
        {"imp/attack", "dsfirsht" },
        {"imp/shotx", "dsfirxpl" },

        // Demon

        {"demon/sight", "dssgtsit" },
        {"demon/active", "dsdmact" },
        {"demon/pain", "dsdmpain" },
        {"demon/melee", "dssgtatk" },
        {"demon/death", "dssgtdth" },

        // Spectre

        {"spectre/sight", "dssgtsit" },
        {"spectre/active", "dsdmact" },
        {"spectre/pain", "dsdmpain" },
        {"spectre/melee", "dssgtatk" },
        {"spectre/death", "dssgtdth" },

        // Cacodemon

        {"caco/sight", "dscacsit" },
        {"caco/active", "dsdmact" },
        {"caco/pain", "dsdmpain" },
        {"caco/death", "dscacdth" },
        {"caco/attack", "dsfirsht" },
        {"caco/shotx", "dsfirxpl" },

        // Baron of Hell

        {"baron/sight", "dsbrssit" },
        {"baron/active", "dsdmact" },
        {"baron/pain", "dsdmpain" },
        {"baron/melee", "dsclaw" },
        {"baron/death", "dsbrsdth" },
        {"baron/attack", "dsfirsht" },
        {"baron/shotx", "dsfirxpl" },

        // Hell Knight

        {"knight/sight", "dskntsit" },
        {"knight/active", "dsdmact" },
        {"knight/pain", "dsdmpain" },
        {"knight/death", "dskntdth" },

        // Lost Soul

        {"skull/active", "dsdmact" },
        {"skull/pain", "dsdmpain" },
        {"skull/melee", "dssklatk" },
        {"skull/death", "dsfirxpl" },

        // Spider Mastermind

        {"spider/sight", "dsspisit" },
        {"spider/active", "dsdmact" },
        {"spider/pain", "dsdmpain" },
        {"spider/attack", "dsshotgn" },
        {"spider/death", "dsspidth" },
        {"spider/walk", "dsmetal" },

        // Arachnotron

        {"baby/sight", "dsbspsit" },
        {"baby/active", "dsbspact" },
        {"baby/pain", "dsdmpain" },
        {"baby/death", "dsbspdth" },
        {"baby/walk", "dsbspwlk" },
        {"baby/attack", "dsplasma" },
        {"baby/shotx", "dsfirxpl" },

        // Cyber Demon

        {"cyber/sight", "dscybsit" },
        {"cyber/active", "dsdmact" },
        {"cyber/pain", "dsdmpain" },
        {"cyber/death", "dscybdth" },
        {"cyber/hoof", "dshoof" },

        // Pain Elemental

        {"pain/sight", "dspesit" },
        {"pain/active", "dsdmact" },
        {"pain/pain", "dspepain" },
        {"pain/death", "dspedth" },

        // Wolfenstein SS

        {"wolfss/sight", "dssssit" },
        {"wolfss/active", "dsposact" },
        {"wolfss/pain", "dspopain" },
        {"wolfss/death", "dsssdth" },
        {"wolfss/attack", "dsshotgn" },

        // Commander Keen

        {"keen/pain", "dskeenpn" },
        {"keen/death", "dskeendt" },

        // Boss Brain

        {"brain/sight", "dsbossit" },
        {"brain/pain", "dsbospn" },
        {"brain/death", "dsbosdth" },
        {"brain/spit", "dsbospit" },
        {"brain/cube", "dsboscub" },
        {"brain/cubeboom", "dsfirxpl" },


        //============================================================================
        //
        // WORLD SOUNDS
        //
        //===========================================================================
        
        {"world/barrelx", "dsbarexp" },

        {"world/drip", "dsempty" },
        {"world/watersplash", "dsempty" },
        {"world/sludgegloop", "dsempty" },
        {"world/lavasizzle", "dsempty" },

        //
        //
        // Platform Sounds
        //

        {"plats/pt1_strt", "dspstart" },
        {"plats/pt1_stop", "dspstop" },
        {"plats/pt1_mid", "dsstnmov" },

        //
        // Door Sounds
        //

        {"doors/dr1_open", "dsdoropn" },
        {"doors/dr1_clos", "dsdorcls" },
        {"doors/dr2_open", "dsbdopn" },
        {"doors/dr2_clos", "dsbdcls" },

        //===========================================================================
        //
        // MISCELLANEOUS SOUNDS
        //
        //===========================================================================

        {"misc/secret", "dssecret" },
        {"misc/w_pkup", "dswpnup" },		// Pickup weapon
        {"misc/p_pkup", "dsgetpow" },	// Pickup powerup
        {"misc/i_pkup", "dsitemup" },	// Pickup item
        {"misc/k_pkup", "dsitemup" },	// Pickup key
        {"misc/spawn", "dsitmbk" },		// Item respawn
        {"misc/chat", "dsradio" },		// Doom 2 chat sound
        {"misc/chat2", "dstink" },		// Chat sound for everything else

        {"switches/normbutn", "dsswtchn" },
        {"switches/exitbutn", "dsswtchx" },

        {"misc/teleport", "dstelept" },

        {"menu/activate", "dsswtchn" },	// Activate a new menu
        {"menu/backup", "dsswtchn" },	// Backup to previous menu
        {"menu/prompt", "dsswtchn" },	// Activate a prompt "menu"
        {"menu/cursor", "dspstop" },		// Move cursor up/down
        {"menu/change", "dsstnmov" },	// Select new value for option
        {"menu/invalid", "dsoof" },		// Menu not available
        {"menu/dismiss", "dsswtchx" },	// Dismiss a prompt message
        {"menu/choose", "dspistol" },	// Choose a menu item
        {"menu/clear", "dsswtchx" },	// Close top menu

    };
}
