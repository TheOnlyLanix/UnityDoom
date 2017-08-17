// Emacs style mode select   -*- C++ -*-
//-----------------------------------------------------------------------------
//
// Copyright(C) 1993-1996 Id Software, Inc.
// Copyright(C) 2005 Simon Howard
// Copyright(C) 2006 Ben Ryves 2006
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
// 02111-1307, USA.
//
// mus2mid.c - Ben Ryves 2006 - http://benryves.com - benryves@benryves.com
// Use to convert a MUS file into a single track, type 0 MIDI file.
// 
// mus2mid.cpp adapted to C++ and SLADE 3 classes, work with MemChunks 
// instead of files. Most boolean functions had their "polarity" reversed:
// the original code returned true on failure, false on success. Now this
// consistently return true on success and false if there were a problem.



using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class mus2mid : MonoBehaviour
{

    enum musevent
    {
        mus_releasekey = 0x00,
        mus_presskey = 0x10,
        mus_pitchwheel = 0x20,
        mus_systemevent = 0x30,
        mus_changecontroller = 0x40,
        mus_scoreend = 0x60
    };

    // MIDI event codes

    static byte midi_releasekey = 0x80;
    static byte midi_presskey = 0x90;
    static byte midi_aftertouchkey = 0xA0;
    static byte midi_changecontroller = 0xB0;
    static byte midi_changepatch = 0xC0;
    static byte midi_aftertouchchannel = 0xD0;
    static byte midi_pitchwheel = 0xE0;



    static byte[] midiheader = new byte[] {
        (byte)'M', (byte)'T', (byte)'h', (byte)'d',		// Main header
        0x00, 0x00, 0x00, 0x06,	// Header size
        0x00, 0x00,				// MIDI type (0)
        0x00, 0x01,				// Number of tracks
        0x00, 0x46,				// Resolution
        (byte)'M', (byte)'T', (byte)'r', (byte)'k',		// Start of track
        0x00, 0x00, 0x00, 0x00	// Placeholder for track length
    };

    // Constants
    static int NUM_CHANNELS = 16;
    static int MUS_PERCUSSION_CHAN = 15;
    static int MIDI_PERCUSSION_CHAN = 9;
    static int MIDI_TRACKLENGTH_OFS = 18;

    // Cached channel velocities
    static byte[] channelvelocities = new byte[]
    {
    127, 127, 127, 127, 127, 127, 127, 127,
    127, 127, 127, 127, 127, 127, 127, 127
    };

    // Timestamps between sequences of MUS events
    static int queuedtime = 0;

    // Counter for the length of the track
    static int tracksize;

    static byte[] controller_map = new byte[]
    {
    0x00, 0x20, 0x01, 0x07, 0x0A, 0x0B, 0x5B, 0x5D,
    0x40, 0x43, 0x78, 0x7B, 0x7E, 0x7F, 0x79
    };

    static int[] channel_map = new int[NUM_CHANNELS];


    static List<byte> midi = new List<byte>();



    public byte[] WriteMidi(MUS mus, string name)
    {
        queuedtime = 0;
        tracksize = 0;
        channel_map = new int[NUM_CHANNELS];
        midi = new List<byte>();

        DoMus2mid(mus);
        return midi.ToArray();
    }




    // Write timestamp to a MIDI file.
    static bool WriteTime(int time)
    {
        int buffer = (time & 0x7F);
        byte writeval;

        while ((time >>= 7) != 0)
        {
            buffer <<= 8;
            buffer |= ((time & 0x7F) | 0x80);
        }

        for (;;)
        {
            writeval = (byte)(buffer & 0xFF);

            midi.Add(writeval);

            tracksize++;

            if ((buffer & 0x80) != 0)
                buffer >>= 8;
            else
            {
                queuedtime = 0;
                return true;
            }
        }
    }

    // Write the end of track marker
    static bool WriteEndTrack()
    {
        byte[] endtrack = new byte[] { 0xFF, 0x2F, 0x00 };

        if (!WriteTime(queuedtime))
            return false;

        midi.AddRange(endtrack);

        tracksize += 3;

        return true;
    }

    // Write a key press event
    static bool WritePressKey(byte channel, byte note, byte velocity)
    {
        // Write queued time
        if (!WriteTime(queuedtime))
            return false;

        // Write pressed key and channel
        byte working = (byte)(midi_presskey | channel);

        midi.Add(working);

        // Write key
        working = (byte)(note & 0x7F);

        midi.Add(working);

        // Wite velocity
        working = (byte)(velocity & 0x7F);

        midi.Add(working);

        tracksize += 3;

        return true;
    }

    // Write a key release event
    static bool WriteReleaseKey(byte channel, byte note)
    {

        // Write queued time
        if (!WriteTime(queuedtime))
            return false;

        // Write released key
        byte working = (byte)(midi_releasekey | channel);

        midi.Add(working);

        // Write key(note)
        working = (byte)(note & 0x7F);
        midi.Add(working);


        // Dummy
        working = (byte)0;

        midi.Add(working);

        tracksize += 3;

        return true;
    }

    // Write a pitch wheel/bend event
    static bool WritePitchWheel(byte channel, int wheel)
    {
        // Write queued time
        if (!WriteTime(queuedtime))
            return false;

        byte working = (byte)(midi_pitchwheel | channel);
        midi.Add(working);

        working = (byte)(wheel & 0x7F);

        midi.Add(working);

        working = (byte)((wheel >> 7) & 0x7F);

        midi.Add(working);

        tracksize += 3;

        return true;
    }

    // Write a patch change event
    static bool WriteChangePatch(byte channel, byte patch)
    {
        // Write queued time
        if (!WriteTime(queuedtime))
            return false;

        byte working = (byte)(midi_changepatch | channel);

        midi.Add(working);

        working = (byte)(patch & 0x7F);
        midi.Add(working);

        tracksize += 2;

        return true;
    }

    // Write a valued controller change event

    static bool WriteChangeController_Valued(byte channel, byte control, byte value)
    {
        // Write queued time
        if (!WriteTime(queuedtime))
            return false;

        byte working = (byte)(midi_changecontroller | channel);
        midi.Add(working);

        working = (byte)(control & 0x7F);
        midi.Add(working);

        // Quirk in vanilla DOOM? MUS controller values should be 7-bit, not 8-bit.
        working = (byte)(((value & 0x80) != 0) ? 0x7F : value);


        midi.Add(working);

        tracksize += 3;

        return true;
    }

    // Write a valueless controller change event
    static bool WriteChangeController_Valueless(byte channel, byte control)
    {
        return WriteChangeController_Valued(channel, control, 0);
    }

    // Allocate a free MIDI channel.
    static int AllocateMIDIChannel()
    {
        int result;
        int max;
        int i;

        // Find the current highest-allocated channel.

        max = -1;

        for (i = 0; i < NUM_CHANNELS; i++)
        {
            if (channel_map[i] > max)
            {
                max = channel_map[i];
            }
        }

        // max is now equal to the highest-allocated MIDI channel.  We can
        // now allocate the next available channel.  This also works if
        // no channels are currently allocated (max=-1)

        result = max + 1;

        // Don't allocate the MIDI percussion channel!

        if (result == MIDI_PERCUSSION_CHAN)
        {
            result++;
        }

        return result;
    }

    // Given a MUS channel number, get the MIDI channel number to use in the outputted file.
    static int GetMIDIChannel(byte mus_channel)
    {
        // Find the MIDI channel to use for this MUS channel.
        // MUS channel 15 is the percusssion channel.

        if (mus_channel == MUS_PERCUSSION_CHAN)
        {
            return MIDI_PERCUSSION_CHAN;
        }
        else
        {
            // If a MIDI channel hasn't been allocated for this MUS channel
            // yet, allocate the next free MIDI channel.

            if (channel_map[mus_channel] == -1)
            {
                channel_map[mus_channel] = AllocateMIDIChannel();
            }

            return channel_map[mus_channel];
        }
    }

    // Read a MUS file from a MemChunk (musinput) and output a MIDI file to
    // a MemChunk (midioutput).
    //
    // Returns true if successful, false otherwise

    public static bool DoMus2mid(MUS mus)
    {

        int channel; // Channel number


        // Buffer used for MIDI track size record
        byte[] tracksizebuffer = new byte[4];

        // Flag for when the score end marker is hit.
        bool hitscoreend = false;


        // Used in building up time delays
        int timedelay;

        // Initialise channel map to mark all channels as unused.
        for (channel = 0; channel < NUM_CHANNELS; channel++)
        {
            channel_map[channel] = -1;
        }

        // Check MUS header
        if (mus.id != "MUS")
        {
            return false;
        }

        // So, we can assume the MUS file is faintly legit. Let's start writing MIDI data...


        midi.AddRange(midiheader); //write the midi header
        tracksize = 0;

        // Now, process the MUS file:
        while (!hitscoreend)
        {

            // Handle a block of events:
            timedelay = 0;

            foreach (Mus_Event mEvent in mus.musEvents)
            {
                // Fetch channel number and event code:

                //channel = mEvent.channelNum;
                channel = GetMIDIChannel(mEvent.channelNum);

                switch (mEvent.musEventType)
                {
                    case 0:
                        WriteReleaseKey((byte)channel, mEvent.note);
                        break;

                    case 1:
                        if (mEvent.vol != 0)
                            channelvelocities[channel] = mEvent.vol;

                        WritePressKey((byte)channel, mEvent.note, (byte)channelvelocities[channel]);
                        break;

                    case 2:
                        WritePitchWheel((byte)channel, mEvent.pitch * 64);
                        break;

                    case 3:

                        if (mEvent.contNum < 10 || mEvent.contNum > 14)
                            continue;

                        WriteChangeController_Valueless((byte)channel, (byte)controller_map[mEvent.contNum]);
                        break;

                    case 4:
                        if (mEvent.contNum == 0)
                            WriteChangePatch((byte)channel, mEvent.contVal);
                        else
                        {
                            if (mEvent.contNum < 1 || mEvent.contNum > 9)
                                continue;

                            WriteChangeController_Valued((byte)channel, (byte)controller_map[mEvent.contNum], mEvent.contVal);

                        }
                        break;

                    case 6:
                        hitscoreend = true;
                        break;

                    default:
                        continue;
                }

                queuedtime = mEvent.time;
            }
        }

        // End of track
        if (!WriteEndTrack())
        {
            return false;
        }

        // Write the track size into the stream


        midi[MIDI_TRACKLENGTH_OFS + 0] = (byte)((tracksize >> 24) & 0xff);
        midi[MIDI_TRACKLENGTH_OFS + 1] = (byte)((tracksize >> 16) & 0xff);
        midi[MIDI_TRACKLENGTH_OFS + 2] = (byte)((tracksize >> 8) & 0xff);
        midi[MIDI_TRACKLENGTH_OFS + 3] = (byte)(tracksize & 0xff);

        //midi.AddRange(tracksizebuffer);

        return true;
    }

}