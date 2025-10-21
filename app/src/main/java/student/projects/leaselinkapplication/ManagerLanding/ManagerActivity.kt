// ManagerActivity.kt
package student.projects.leaselinkapplication.ManagerLanding

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.google.android.material.button.MaterialButton
import student.projects.leaselinkapplication.R

class ManagerActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_manager)

        val sendBroadcastBtn = findViewById<MaterialButton>(R.id.buttonSendBroadcast)
        val sendReminderBtn  = findViewById<MaterialButton>(R.id.buttonSendReminder)
        val updateListingsBtn = findViewById<MaterialButton>(R.id.buttonUpdateListings)
        val settingsBtn      = findViewById<MaterialButton>(R.id.buttonSettings)

        // These targets are in the same package: student.projects.leaselinkapplication.ManagerLanding
        sendBroadcastBtn.setOnClickListener {
            startActivity(Intent(this, SendBroadcastActivity::class.java))
        }

        sendReminderBtn.setOnClickListener {
            startActivity(Intent(this, SendReminderActivity::class.java))
        }

        updateListingsBtn.setOnClickListener {
            startActivity(Intent(this, UpdateListingsActivity::class.java))
        }

        // Settings button left blank intentionally (hook up when you have a SettingsActivity)
        settingsBtn.setOnClickListener {
            // TODO: startActivity(Intent(this, SettingsActivity::class.java))
        }
    }
}
